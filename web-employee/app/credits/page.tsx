'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { FaKey } from 'react-icons/fa';
import { MdAddCircleOutline, MdOutlineDisabledByDefault } from 'react-icons/md';
import { useSWRConfig } from 'swr';
import { api } from '../api';
import { Credit } from '../api/types';
import { animationVariants } from '../config';
import { useBankFetch } from '../hooks/useBankFetch';
import AddCredModal from './components/AddCredModal';

const Credits: FC = () => {
    const mockCredits: Credit[] = [
        {
            id: '1',
            name: 'Кредит 1',
            percent: 10,
            maxCreditSum: 1000000,
            maxRepaymentPeriod: 1,
            minCreditSum: 1,
            minRepaymentPeriod: 1,
            paymentType: 1,
            pennyPercent: 1,
            rateType: 1,
        },
        {
            id: '2',
            name: 'Кредит 2',
            percent: 10,
            maxCreditSum: 1000000,
            maxRepaymentPeriod: 1,
            minCreditSum: 1,
            minRepaymentPeriod: 1,
            paymentType: 1,
            pennyPercent: 1,
            rateType: 1,
        },
    ];

    const [isAddOpen, openAdd] = useState(false);
    const [credit, choseCredit] = useState<Credit>();

    const { result: credits, error, mock } = useBankFetch<Credit[]>('/api/credits', api.credits.getAll, mockCredits);

    const { mutate } = useSWRConfig();

    const handleDelete = async () => {
        if (!credit) return;
        const isYes = confirm('Вы уверены, что хотите удалить ' + credit.name + '?');
        if (isYes)
            try {
                await api.credits.delete(credit.id);
                mutate('/api/credits', async (prev: any) => {
                    return prev.filter((c: any) => c.id !== credit.id);
                });
            } catch (error) {}
    };

    return (
        <>
            <AddCredModal isOpen={isAddOpen} onClose={() => openAdd(false)} />
            <div className="flex-center gap-2">
                <button
                    onClick={() => openAdd(true)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-gray-900 text-white"
                >
                    Добавить
                    <MdAddCircleOutline />
                </button>
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
            </div>
            {error && <p>Ошибка сервера: {error.message}</p>}
            {credits && credits.length == 0 && <h1 className="font-medium text-3xl text-center">Нет кредитов</h1>}
            {credits && credits.length && (
                <motion.ul
                    className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 overflow-hidden pt-5"
                    variants={animationVariants.container}
                    initial="hidden"
                    animate="visible"
                >
                    {credits.map((cred, index) => (
                        <motion.li
                            key={index}
                            variants={animationVariants.item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 bg-fuchsia-950 h-auto gap-1`}
                        >
                            <div key={index} className="flex w-full text-3xl justify-between">
                                <p
                                    style={{
                                        color: 'rgb(255, 0,255)',
                                    }}
                                    className={`self-center`}
                                >
                                    {cred.name}
                                </p>
                                <div className="flex gap-2 items-center">
                                    <MdOutlineDisabledByDefault
                                        cursor={'pointer'}
                                        color="red"
                                        size={36}
                                        onClick={e => {
                                            choseCredit(cred);
                                            handleDelete();
                                            e.preventDefault();
                                        }}
                                    />
                                </div>
                            </div>
                            <div className="h-full py-2 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                                <p className={`text-white w-full`}>Ставка {cred.percent}%</p>
                                <p className={`text-white w-full`}>Процент пенни: {cred.pennyPercent}%</p>
                                <p className={`text-white w-full`}>Мин период: {cred.minRepaymentPeriod} мес.</p>
                                <p className={`text-white w-full`}>Макс период: {cred.maxRepaymentPeriod} мес.</p>
                                <p className={`text-white w-full`}>Мин сумма: {cred.minCreditSum}</p>
                                <p className={`text-white w-full`}>Макс сумма: {cred.maxCreditSum}</p>
                            </div>
                        </motion.li>
                    ))}
                </motion.ul>
            )}
        </>
    );
};

export default Credits;
