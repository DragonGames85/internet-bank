'use client';
import { motion } from 'framer-motion';
import Link from 'next/link';
import { FC, useEffect, useState } from 'react';
import { FaPen } from 'react-icons/fa';
import { MdAddCircleOutline, MdOutlineDisabledByDefault } from 'react-icons/md';
import AddCredModal from './components/AddCredModal';
import { IdName } from '../config';
import DeleteCredModal from './components/DeleteCredModal';
import EditCredModal from './components/EditCredModal';

const container = {
    hidden: { opacity: 1, scale: 0 },
    visible: {
        opacity: 1,
        scale: 1,
        transition: {
            delayChildren: 0.1,
            staggerChildren: 0.1,
        },
    },
};

const item = {
    hidden: { y: 20, opacity: 0 },
    visible: {
        y: 0,
        opacity: 1,
    },
};

const creditsArray = [
    {
        id: 1,
        name: 'Кредит 1',
        percent: 10,
    },
    {
        id: 2,
        name: 'Кредит 2',
        percent: 20,
    },
    {
        id: 3,
        name: 'Кредит 3',
        percent: 30,
    },
    {
        id: 4,
        name: 'Кредит 4',
        percent: 40,
    },
    {
        id: 5,
        name: 'Кредит 5',
        percent: 50,
    },
];

const Credits: FC = () => {
    useEffect(() => {
        document.documentElement.style.setProperty('--background-end-rgb', '120 0 120');
    }, []);

    const [isAddOpen, openAdd] = useState(false);
    const [isDelOpen, openDel] = useState(false);
    const [isEditOpen, openEdit] = useState(false);
    const [credit, choseCredit] = useState<IdName & { percent: number }>();

    return (
        <>
            <AddCredModal isOpen={isAddOpen} onClose={() => openAdd(false)} />
            <EditCredModal
                isOpen={isEditOpen}
                onClose={() => {
                    openEdit(false);
                    choseCredit(undefined);
                }}
                credit={credit}
            />
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
            <motion.ul
                className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 overflow-hidden pt-5"
                variants={container}
                initial="hidden"
                animate="visible"
            >
                {creditsArray.map((cred, index) => (
                    <Link key={index} href={`/credits/${cred.id}`}>
                        <motion.li
                            variants={item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 bg-fuchsia-950 h-auto gap-1`}
                        >
                            <div key={index} className="flex w-full text-3xl justify-between">
                                <p
                                    style={{
                                        color: 'rgb(255, 0,255)',
                                    }}
                                    className={`self-center underline underline-offset-8`}
                                >
                                    {cred.name}
                                </p>
                                <div className="flex gap-2 items-center">
                                    <FaPen
                                        cursor={'pointer'}
                                        color="white"
                                        size={24}
                                        onClick={e => {
                                            choseCredit(cred);
                                            openEdit(true);
                                            e.preventDefault();
                                        }}
                                    />
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
                            <div className="h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                                <p className={`text-white w-full`}>Ставка {cred.percent}%</p>
                            </div>
                        </motion.li>
                    </Link>
                ))}
            </motion.ul>
        </>
    );
};

export default Credits;
