'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { FaKey } from 'react-icons/fa';
import { FaMoneyBillTransfer } from 'react-icons/fa6';
import useSWR from 'swr';
import { api } from '../api';
import { Credit, CurrencyArray, CurrencySymbol, User } from '../api/types';
import { animationVariants } from '../config';

const Credits: FC = () => {
    const { data } = useSWR('/api/credits', api.credits.getAll);
    const [mocked, mock] = useState(false);

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

    const credits = mocked ? mockCredits : data;

    return !(credits && !!credits.length) ? (
        <>
            <h1 className="font-medium text-3xl text-center">Нет кредитов</h1>
            <button
                onClick={() => mock(prev => !prev)}
                className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
            >
                Мок
                <FaKey color={'fuchsia'} />
            </button>
        </>
    ) : (
        <>
            <button
                onClick={() => mock(prev => !prev)}
                className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
            >
                Мок
                <FaKey color={'fuchsia'} />
            </button>
            <motion.ul
                className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5"
                variants={animationVariants.container}
                initial="hidden"
                animate="visible"
            >
                {credits.map((cred, index) => (
                    <motion.li
                        key={index}
                        variants={animationVariants.item}
                        className={`flex items-center justify-between border-[1px] rounded-3xl p-6 bg-bgColor2 dark:bg-bgColor2Dark h-auto gap-1`}
                    >
                        <div>
                            <p
                                style={{
                                    color: 'rgb(255, 0,255)',
                                }}
                                className={`text-3xl `}
                            >
                                {cred.name}
                            </p>
                            <p className={`dark:text-white text-black w-full`}>Ставка {cred.percent}%</p>
                            <p className={`dark:text-white text-black w-full`}>Процент пенни: {cred.pennyPercent}%</p>
                            <p className={`dark:text-white text-black w-full`}>
                                Мин период: {cred.minRepaymentPeriod} мес.
                            </p>
                            <p className={`dark:text-white text-black w-full`}>
                                Макс период: {cred.maxRepaymentPeriod} мес.
                            </p>
                            <p className={`dark:text-white text-black w-full`}>Мин сумма: {cred.minCreditSum}</p>
                            <p className={`dark:text-white text-black w-full`}>Макс сумма: {cred.maxCreditSum}</p>
                        </div>
                        <button
                            onClick={async () => {
                                const result = Number(prompt('Сколько хотите взять?'));
                                const repaymentPeriod = Number(prompt('На сколько месяцев хотите взять?'));
                                let currencyResult = prompt('Валюта? (напр. RUB)') as CurrencySymbol;
                                const userLocal: User = JSON.parse(localStorage.getItem('user') ?? '');
                                currencyResult = CurrencyArray.includes(currencyResult) ? currencyResult : 'RUB';
                                await api.credits.sub({
                                    userId: userLocal ? userLocal.id : '1',
                                    currency: currencyResult,
                                    value: result,
                                    tariffId: cred.id,
                                    repaymentPeriod,
                                    paymentPeriod: 1,
                                });
                            }}
                            className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-purple-400 bg-white dark:bg-mainText"
                        >
                            Взять <FaMoneyBillTransfer />
                        </button>
                    </motion.li>
                ))}
            </motion.ul>
        </>
    );
};

export default Credits;
