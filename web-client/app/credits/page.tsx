'use client';
import { motion } from 'framer-motion';
import { FC } from 'react';
import { FaFlagCheckered } from 'react-icons/fa';
import { FaMoneyBillTransfer } from 'react-icons/fa6';
import { useColorEffect } from '../hooks/useColorEffect';
import { animationVariants } from '../config';
import useSWR from 'swr';
import { api } from '../api';

const Credits: FC = () => {
    useColorEffect('120 0 120');
    const { data: credits } = useSWR('/api/credits', api.credits.getAll);

    return !(credits && !!credits.length) ? (
        <h1>Нет кредитов</h1>
    ) : (
        <motion.ul
            className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5"
            variants={animationVariants.container}
            initial="hidden"
            animate="visible"
        >
            {credits.map((cred, index) => (
                <motion.li
                    variants={animationVariants.item}
                    className={`flex items-center justify-between border-[1px] rounded-3xl p-6 bg-fuchsia-950 h-auto gap-1`}
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
                        <p className={`text-white w-full py-2 text-xl text-ellipsis`}>Ставка {cred.percent}%</p>
                    </div>
                    {index % 2 != 0 ? (
                        <button
                            onClick={async () => {
                                await api.credits.close(cred.id);
                            }}
                            className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-red-500"
                        >
                            Погасить <FaFlagCheckered />
                        </button>
                    ) : (
                        <button
                            onClick={async () => {
                                const result = Number(prompt('Сколько хотите взять?'));
                                await api.credits.sub({
                                    userId: '1',
                                    currency: 'RUB',
                                    value: result,
                                    tariffId: cred.id,
                                    repaymentPeriod: 24,
                                    paymentPeriod: 1,
                                });
                            }}
                            className="flex items-center gap-2 border-[1px] rounded-full p-2 px-4 text-xl text-purple-400"
                        >
                            Взять <FaMoneyBillTransfer />
                        </button>
                    )}
                </motion.li>
            ))}
        </motion.ul>
    );
};

export default Credits;
