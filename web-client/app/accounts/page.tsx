'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { FaKey } from 'react-icons/fa';
import { MdAddCircleOutline } from 'react-icons/md';
import { api } from '../api';
import { Account } from '../api/types';
import { animationVariants } from '../config';
import { useBankFetch } from '../hooks/useBankFetch';
import AccCard from './components/AccCard';
import AddAccModal from './components/AddAccModal';

const Credits: FC = () => {
    const [isAddOpen, openAdd] = useState(false);

    const mockAccounts: Account[] = [
        {
            balance: 0,
            id: '23',
            number: '2',
            type: 0,
            user: { id: '2', name: 'dANIL' },
            currency: { id: '1', name: 'RUB', symbol: 'USD' },
        },
    ];

    const { result: accounts, mock } = useBankFetch<Account[]>('/api/accounts', api.accounts.getAll, mockAccounts);

    return (
        <>
            <AddAccModal isOpen={isAddOpen} onClose={() => openAdd(false)} />
            <div className="flex justify-center w-full gap-4 text-center items-center">
                <button
                    onClick={() => openAdd(true)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Открыть
                    <MdAddCircleOutline color={'green'} />
                </button>
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl p-2 flex gap-2 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
            </div>
            {accounts && !!accounts.length && (
                <motion.ul
                    className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5"
                    variants={animationVariants.container}
                    initial="hidden"
                    animate="visible"
                >
                    {accounts.map((acc, index) => (
                        <motion.li
                            key={index}
                            variants={animationVariants.item}
                            className={`flex items-center justify-between border-[1px] rounded-3xl p-6 h-auto gap-1 bg-bgColor2 dark:bg-bgColorDark`}
                        >
                            <AccCard {...acc} />
                        </motion.li>
                    ))}
                </motion.ul>
            )}
        </>
    );
};

export default Credits;
