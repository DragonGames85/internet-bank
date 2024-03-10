import { api } from '@/app/api';
import { Account } from '@/app/api/types';
import Link from 'next/link';
import { FC } from 'react';
import { IoArrowDownCircle, IoArrowUpCircle } from 'react-icons/io5';
import { MdOutlineDisabledByDefault } from 'react-icons/md';

const AccCard: FC<Account> = ({ balance, id, currency }) => {
    function add(e: React.MouseEvent<HTMLElement>) {
        const result = Number(prompt('Сколько хотите положить?'));
        api.operations.post({
            currencyName: currency.name,
            name: 'Пополнение',
            receiveAccountNumber: id,
            value: result,
        });
    }
    function remove(e: React.MouseEvent<HTMLElement>) {
        const result = Number(prompt('Сколько хотите снять?'));
        if (balance - result < 0) {
            alert('Счёт не может быть отрицательным');
            return;
        }
        api.operations.post({
            currencyName: currency.name,
            name: 'Снятие',
            sendAccountNumber: id,
            value: result,
        });
    }

    return (
        <>
            <div className="flex flex-col justify-center">
                <div className={`text-3xl text-cyan-500 flex items-center gap-2`}>
                    <Link href={`/accounts/${id}`} className="underline underline-offset-[6px]">
                        {id}
                    </Link>
                    <MdOutlineDisabledByDefault
                        onClick={async () => {
                            if (confirm('Закрыть счёт?')) {
                                await api.accounts.delete(id);
                            } else {
                                return;
                            }
                        }}
                        color="red"
                        className="text-4xl mt-1 cursor-pointer"
                    />
                </div>
                <p className={`text-white w-full py-2 text-xl text-ellipsis`}>Баланс {balance} руб</p>
            </div>
            <div className="flex flex-col gap-2 sm:flex-row md:flex-row lg:flex-col xl:flex-col 2xl:flex-col">
                <button
                    onClick={add}
                    className="flex items-center justify-center gap-2 border-[1px] rounded-full p-2 text-xl text-green-400 hover:bg-gray-600"
                >
                    Пополнить <IoArrowUpCircle />
                </button>
                <button
                    onClick={remove}
                    className="flex items-center justify-center gap-2 border-[1px] rounded-full p-2 text-xl text-red-500 hover:bg-gray-600"
                >
                    Снять <IoArrowDownCircle />
                </button>
            </div>
        </>
    );
};

export default AccCard;
