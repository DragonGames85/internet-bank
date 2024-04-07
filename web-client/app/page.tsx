'use client';

import human from '@/assets/human.png';

import Image from 'next/image';

import { useSearchParams } from 'next/navigation';
import Login from './components/Login';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './hooks/useLocalStorage';
import { api } from './api';
import useSWR, { useSWRConfig } from 'swr';
import { FaFlagCheckered } from 'react-icons/fa';

export default function Home() {
    const token = useSearchParams().get('token') ?? '';
    const [localToken, _] = useLocalStorage('token', '');
    const user = localToken ? parseJwt(localToken) : {};

    const { mutate } = useSWRConfig();

    const { data: myCredits } = useSWR(token || localToken ? '/api/creditsMy' : null, () =>
        api.credits.getUserCredits(user.id)
    );
    const { data: expCredits } = useSWR(token || localToken ? '/api/expired_credits' : null, () =>
        api.credits.expired(user.id)
    );

    return token || localToken ? (
        <div className="flex flex-col w-full items-center gap-12">
            <Image
                width={400}
                height={400}
                src={human}
                alt="profile-image"
                className="rounded-full border-[3px] object-cover pointer-events-none text-2xl select-none"
            />
            <div className="flex flex-col gap-2">
                <p className="text-xl mb-12">Вы авторизированны - {user.name}</p>
                <div>
                    <h3 className="text-2xl font-semibold mb-2">Мои кредитные тарифы:</h3>
                    <ul className="text-lg font-medium">
                        {(!myCredits || myCredits?.length == 0) && <li>отсутствует</li>}
                        {myCredits?.map((cred, ind) => (
                            <li className="flex items-center justify-between mb-4">
                                <p>
                                    {ind + 1}. {cred.tariffName ?? cred.name}
                                </p>
                                <button
                                    onClick={async () => {
                                        await api.credits.close(cred.id);
                                        mutate('/api/creditsMy');
                                    }}
                                    className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-red-500 bg-white dark:bg-mainText"
                                >
                                    Погасить <FaFlagCheckered />
                                </button>
                            </li>
                        ))}
                    </ul>
                    <h3 className="text-2xl mt-12 font-semibold">Мои просроченные кредиты</h3>
                    <ul>
                        {(!expCredits || expCredits?.length == 0) && <li>отсутствует</li>}
                        {expCredits?.map((cred, ind) => (
                            <li>
                                {ind + 1}. {cred.tariffName ?? cred.name}
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    ) : (
        <div className="w-full flex-center">
            <Login />
        </div>
    );
}
