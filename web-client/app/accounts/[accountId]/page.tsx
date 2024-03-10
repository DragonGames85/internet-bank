'use client';

import { api } from '@/app/api';
import { useColorEffect } from '@/app/hooks/useColorEffect';
import useSWR from 'swr';

const Accounts = ({ params }: { params: { accountId: string } }) => {
    useColorEffect('71, 85, 105');

    const { data: account } = useSWR('/api/accounts', () => api.accounts.get(params.accountId));
    const { data: operations } = useSWR('/api/operations', () => api.operations.getAll(params.accountId));

    if (account)
        return (
            <div className="w-full flex justify-center items-center flex-col text-white">
                <div
                    className={`flex flex-col border-[1px] rounded-3xl p-6 bg-slate-600 gap-1 w-full 2xl:w-[50%] items-center`}
                >
                    <h2 className="text-2xl">{`Счёт "${account.id}"`}</h2>
                    <div className="flex flex-col items-center sm:flex-row justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                        <p>Владелец: {account.user.name}</p>
                        <p>Баланс: {account.balance}$</p>
                    </div>
                </div>
                <h2 className="text-center mt-16 text-4xl mb-6">История операции:</h2>
                {operations?.map(operation => (
                    <div className="flex flex-col sm:flex-row justify-between h-full bg-slate-600 py-4 mt-4 w-full 2xl:w-[60%] self-center text-xl overflow-hidden text-ellipsis items-center relative px-6 border-[1px] rounded-3xl">
                        <p>Сумма: {operation.value}$</p>
                        <p
                            className={`h-full ${
                                !!operation.value ? 'bg-green-400' : 'bg-red-400'
                            } sm:absolute right-0 w-full sm:w-[40%] md:w-[35%] lg:w-[20%] flex items-center justify-center font-bold`}
                        >
                            {!!operation.value ? 'ПОПОЛНЕНИЕ' : 'СНЯТИЕ'}
                        </p>
                    </div>
                ))}
            </div>
        );
};

export default Accounts;
