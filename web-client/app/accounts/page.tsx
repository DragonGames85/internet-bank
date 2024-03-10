'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { MdAddCircleOutline } from 'react-icons/md';
import useSWR from 'swr';
import { api } from '../api';
import { animationVariants } from '../config';
import { useColorEffect } from '../hooks/useColorEffect';
import AccCard from './components/AccCard';
import AddAccModal from './components/AddAccModal';

const Credits: FC = () => {
    const [isAddOpen, openAdd] = useState(false);

    const { data: accounts } = useSWR('/api/accounts', api.accounts.getAll);

    useColorEffect('71, 80, 105');

    return (
        <>
            <AddAccModal isOpen={isAddOpen} onClose={() => openAdd(false)} />
            <button
                onClick={() => openAdd(true)}
                className="text-3xl mx-auto border-2 rounded-2xl p-2 flex gap-2 items-center bg-gray-900"
            >
                Открыть
                <MdAddCircleOutline />
            </button>
            {accounts && accounts.length && (
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
                            className={`flex items-center justify-between border-[1px] rounded-3xl p-6 bg-gray-800 h-auto gap-1`}
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
