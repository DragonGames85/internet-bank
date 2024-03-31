'use client';

import { api } from '@/app/api';
import { Account } from '@/app/api/types';
import { useBankFetch } from '@/app/hooks/useBankFetch';
import { FaKey } from 'react-icons/fa';
import OperationList from '../components/OperationList';

const Accounts = ({ params }: { params: { accountId: string } }) => {
    const mockedAcc: Account = {
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

    const { result: account, mock } = useBankFetch<Account>(
        '/api/account',
        () => api.accounts.get(params.accountId),
        mockedAcc
    );

    console.log(account);

    const user = JSON.parse(localStorage.getItem('user') ?? '');

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
                            <p>Владелец: {account.user.name == '' ? user.name : account.user.name}</p>
                            <p>Баланс: {account.balance} {account.currency.symbol}</p>
                        </div>
                    </div>
                    <OperationList accId={account.id} />
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
