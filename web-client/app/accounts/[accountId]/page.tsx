'use client';

import { LOREM } from '@/app/config';
import { useEffect } from 'react';
import { MdOutlineDisabledByDefault } from 'react-icons/md';

const Accounts = ({ params }: { params: { accountId: string } }) => {
    useEffect(() => {
        document.documentElement.style.setProperty('--background-end-rgb', '71, 85, 105');
    }, []);

    return (
        <div className="w-full flex justify-center items-center flex-col text-white">
            <div className={`flex flex-col border-[1px] rounded-3xl p-6 bg-slate-600 gap-1 w-[50%] items-center`}>
                <div className="flex text-2xl justify-between">
                    <p className={`underline underline-offset-8`}>{`Счёт ${params.accountId}`}</p>
                </div>
                <div className="flex justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                    <p>Владелец: {'ИМЯ'}</p>
                    <p>Баланс: {'50.000'}$</p>
                </div>
            </div>
            <h2 className="text-center mt-16 text-4xl mb-6">История операции:</h2>
            <div className={`flex flex-col border-[1px] rounded-3xl bg-slate-600 gap-1 w-[50%] items-center `}>
                <div className="flex justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis items-center relative px-6">
                    <p>Сумма: {'50.000'}$</p>
                    <p className="h-full bg-green-400 absolute right-0 w-[20%] flex items-center justify-center font-bold">
                        ПОПОЛНЕНИЕ
                    </p>
                </div>
            </div>
            <div className={`flex flex-col border-[1px] rounded-3xl bg-slate-600 gap-1 w-[50%] items-center mt-4`}>
                <div className="flex justify-between h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis items-center relative px-6">
                    <p>Сумма: {'50.000'}$</p>
                    <p className="h-full bg-red-400 absolute right-0 w-[20%] flex items-center justify-center font-bold">
                        СНЯТИЕ
                    </p>
                </div>
            </div>
        </div>
    );
};

export default Accounts;
