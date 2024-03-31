'use client';

import { api } from '@/app/api';
import useSWR from 'swr';
import OperationList from './components/OperationList';

const Accounts = ({ params }: { params: { accountId: string } }) => {
    const { data: account } = useSWR(`/api/accounts/${params.accountId}`, () => api.accounts.account(params.accountId));

    if (account)
        return (
            <div className="w-full flex justify-center items-center flex-col text-white">
                <div
                    className={`flex flex-col border-[1px] rounded-3xl p-6 bg-slate-600 gap-1 w-full 2xl:w-[50%] items-center`}
                >
                    <h2 className="text-2xl">{`Счёт "${account.id}"`}</h2>
                    <div className="flex flex-col items-center sm:flex-row justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                        <p>Владелец: {account.user.id}</p>
                        <p>Баланс: {account.balance}$</p>
                    </div>
                </div>
                <OperationList accId={params.accountId} />
            </div>
        );
};

export default Accounts;
