'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { FaFlagCheckered, FaKey } from 'react-icons/fa';
import { FaMoneyBillTransfer } from 'react-icons/fa6';
import { animationVariants } from '../config';
import useSWR from 'swr';
import { api } from '../api';
import { Credit } from '../api/types';

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
                            <p className={`w-full py-2 text-xl text-ellipsis`}>Ставка {cred.percent}%</p>
                        </div>
                        <div>
                            <button
                                onClick={async () => {
                                    await api.credits.close(cred.id);
                                }}
                                className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-red-500 bg-white dark:bg-mainText"
                            >
                                Погасить <FaFlagCheckered />
                            </button>
                            <button
                                onClick={async () => {
                                    const result = Number(prompt('Сколько хотите взять?'));
                                    await api.credits.sub({
                                        userId: localStorage.getItem('userId') ?? '1',
                                        currency: 'RUB',
                                        value: result,
                                        tariffId: cred.id,
                                        repaymentPeriod: 24,
                                        paymentPeriod: 1,
                                    });
                                }}
                                className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-purple-400 bg-white dark:bg-mainText"
                            >
                                Взять <FaMoneyBillTransfer />
                            </button>
                        </div>
                    </motion.li>
                ))}
            </motion.ul>
        </>
    );
};

export default Credits;
