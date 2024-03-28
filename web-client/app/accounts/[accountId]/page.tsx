'use client';

import { api } from '@/app/api';
import { Account, Operation } from '@/app/api/types';
import { useState } from 'react';
import { FaKey } from 'react-icons/fa';
import useSWR from 'swr';

const Accounts = ({ params }: { params: { accountId: string } }) => {
    const { data: account } = useSWR('/api/accounts', () => api.accounts.get(params.accountId));
    const { data: operations } = useSWR('/api/operations', () => api.operations.getAll(params.accountId));
    const [mocked, mock] = useState(false);

    const mockedAccount: Account = {
        id: '1',
        balance: 1000,
        user: {
            id: '1',
            name: 'Василий',
        },
        currency: { id: '1', name: 'USD', symbol: 'USD' },
        number: '1234567890',
        type: 0,
    };
    const mockedOperations: Operation[] = [
        {
            id: '1',
            currency: { id: '1', name: 'USD', symbol: 'USD' },
            value: 1000,
        },
        {
            id: '2',
            currency: { id: '1', name: 'USD', symbol: 'USD' },
            value: -500,
        },
    ];

    if (mocked)
        return (
            <>
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
                <div className="w-full flex justify-center items-center flex-col">
                    <div
                        className={`flex flex-col border-[1px] rounded-3xl p-6 bg-bgColor2 dark:bg-bgColor2Dark gap-1 w-full 2xl:w-[50%] items-center`}
                    >
                        <h2 className="text-2xl">{`Счёт "${mockedAccount.id}"`}</h2>
                        <div className="flex flex-col items-center sm:flex-row justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                            <p>Владелец: {mockedAccount.user.name}</p>
                            <p>Баланс: {mockedAccount.balance}$</p>
                        </div>
                    </div>
                    <h2 className="text-center mt-16 text-4xl mb-6">История операции:</h2>
                    {mockedOperations?.map(operation => (
                        <div className="flex flex-col sm:flex-row justify-between h-full bg-bgColor2 dark:bg-bgColor2Dark py-4 mt-4 w-full 2xl:w-[60%] self-center text-xl overflow-hidden text-ellipsis items-center relative px-6 border-[1px] rounded-3xl">
                            <p>Сумма: {operation.value}$</p>
                            <p
                                className={`h-full ${
                                    operation.value > 0 ? 'bg-green-400' : 'bg-red-400'
                                } sm:absolute right-0 w-full sm:w-[40%] md:w-[35%] lg:w-[20%] flex items-center justify-center font-bold`}
                            >
                                {operation.value > 0 ? 'ПОПОЛНЕНИЕ' : 'СНЯТИЕ'}
                            </p>
                        </div>
                    ))}
                </div>
            </>
        );
    if (account)
        return (
            <>
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
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
            </>
        );
    else
        return (
            <>
                <p className="font-medium text-3xl text-center">Нет данных</p>
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
            </>
        );
};

export default Accounts;
