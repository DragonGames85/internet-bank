'use client';
import { motion } from 'framer-motion';
import { FC, useState } from 'react';
import { FaPen } from 'react-icons/fa';
import { MdAddCircleOutline, MdOutlineDisabledByDefault } from 'react-icons/md';
import useSWR from 'swr';
import { api } from '../api';
import { Credit } from '../api/types';
import { animationVariants } from '../config';
import { useColorEffect } from '../hooks/useColorEffect';
import AddCredModal from './components/AddCredModal';
import DeleteCredModal from './components/DeleteCredModal';

const Credits: FC = () => {
    useColorEffect('120 0 120');

    const { data: credits } = useSWR<Credit[]>('/api/credits', api.credits.getAll);

    const [isAddOpen, openAdd] = useState(false);
    const [isDelOpen, openDel] = useState(false);
    const [credit, choseCredit] = useState<Credit>();

    return (
        <>
            <AddCredModal isOpen={isAddOpen} onClose={() => openAdd(false)} />
            <DeleteCredModal
                isOpen={isDelOpen}
                onClose={() => {
                    openDel(false);
                    choseCredit(undefined);
                }}
                credit={credit}
            />
            <button
                onClick={() => openAdd(true)}
                className="text-3xl mx-auto border-2 rounded-2xl p-2 flex gap-2 items-center bg-gray-900"
            >
                Добавить
                <MdAddCircleOutline />
            </button>
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
                            className={`flex     flex-col items-start border-[1px] rounded-3xl p-6 bg-fuchsia-950 h-auto gap-1`}
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
                                            openDel(true);
                                            e.preventDefault();
                                        }}
                                    />
                                </div>
                            </div>
                            <div className="h-full py-2 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                                <p className={`text-white w-full`}>Ставка {cred.percent}%</p>
                            </div>
                        </motion.li>
                    ))}
                </motion.ul>
            )}
        </>
    );
};

export default Credits;
