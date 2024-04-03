'use client';

import human from '@/assets/human.png';

import Image from 'next/image';

import { useSearchParams } from 'next/navigation';
import Login from './components/Login';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './hooks/useLocalStorage';

export default function Home() {
    const token = useSearchParams().get('token') ?? '';
    const [localToken, _] = useLocalStorage('token', '');
    const user = localToken ? parseJwt(localToken) : {};

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
                <p className="text-xl">Вы авторизированны - {user.name}</p>
            </div>
        </div>
    ) : (
        <div className="w-full flex-center">
            <Login />
        </div>
    );
}
