import { api } from '@/app/api';
import { Account } from '@/app/api/types';
import Link from 'next/link';
import { FC } from 'react';
import { IoArrowDownCircle, IoArrowUpCircle } from 'react-icons/io5';
import { MdOutlineDisabledByDefault } from 'react-icons/md';
import { IoEyeOutline } from 'react-icons/io5';
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

    function transfer(e: React.MouseEvent<HTMLElement>) {
        const transferId = prompt('ID счёта на перевод') ?? '';
        const transferValue = Number(prompt('Сколько хотите перевести?'));
        if (balance - transferValue < 0) {
            alert('Счёт не может быть отрицательным');
            return;
        }
        api.operations.post({
            currencyName: currency.name,
            name: 'Снятие',
            sendAccountNumber: id,
            receiveAccountNumber: transferId,
            value: transferValue,
        });
    }

    const buttonStyle =
        'flex-center gap-2 border-1 rounded-full p-2 text-xl bg-white hover:bg-gray-200 dark:bg-bgColor2Dark dark:hover:bg-bgColor3Dark';

    return (
        <>
            <div className="flex flex-col justify-center">
                <div className={`text-3xl text-cyan-500 flex items-center gap-2`}>
                    <Link
                        href={`/accounts/${id}`}
                        className="underline underline-offset-[6px] p-2 bg-black dark:bg-bgColor2Dark border-white border-1 rounded-xl"
                    >
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
                <p className={`w-full py-2 text-xl text-ellipsis`}>Баланс: {balance} руб</p>
                <button
                    onClick={() => {}}
                    className="text-xl border-2 rounded-full p-2 flex gap-2 items-center bg-white hover:bg-gray-200 dark:bg-bgColor3 dark:hover:bg-gray-400"
                >
                    Скрыть
                    <IoEyeOutline />
                </button>
            </div>
            <div className="flex flex-col gap-2 sm:flex-row md:flex-row lg:flex-col xl:flex-col 2xl:flex-col">
                <button onClick={add} className={`${buttonStyle} text-green-400`}>
                    Пополнить <IoArrowUpCircle />
                </button>
                <button onClick={remove} className={`${buttonStyle} text-red-500`}>
                    Снять <IoArrowDownCircle />
                </button>
                <button onClick={transfer} className={`${buttonStyle} text-purple-500`}>
                    Перевести <IoArrowDownCircle style={{ transform: 'rotate(-90deg)' }} />
                </button>
            </div>
        </>
    );
};

export default AccCard;
