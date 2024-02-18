import { AnimatePresence, motion } from 'framer-motion';
import Link from 'next/link';
import { FC } from 'react';
import { FaPen } from 'react-icons/fa';
import { MdOutlineDisabledByDefault } from 'react-icons/md';
import { IdName, LOREM, adaptiveClass } from '../config';

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

interface IUserGrid {
    isCoop: boolean;
    array: IdName[];
    choseUser: (user: IdName) => void;
    openEdit: () => void;
    openDel: () => void;
}

const UserGrid: FC<IUserGrid> = ({ array, isCoop, choseUser, openDel, openEdit }) => {
    let colsClass = '';

    colsClass = adaptiveClass('grid-cols', [1, 3, 1, 3, 1, 3]);

    if (colsClass)
        return (
            <AnimatePresence>
                <motion.ul
                    className={`grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5`}
                    key={isCoop ? 'coops' : 'people'}
                    variants={container}
                    initial="hidden"
                    animate="visible"
                >
                    {array.length === 0 && (
                        <motion.li
                            key={0}
                            variants={item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 
                        ${isCoop ? 'bg-blue-950' : 'bg-green-950'}
                        h-auto gap-1`}
                        >
                            <p className="text-3xl">Нет пользователей</p>
                        </motion.li>
                    )}
                    {array.map((user, index) => (
                        <motion.li
                            key={index}
                            variants={item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 
                        ${isCoop ? 'bg-blue-950' : 'bg-green-950'}
                        h-auto gap-1`}
                        >
                            <div key={index} className="flex w-full text-3xl justify-between">
                                <p
                                    style={{
                                        color: isCoop ? 'rgb(0, 255, 255)' : 'rgb(0, 255,0)',
                                    }}
                                    className={`self-center underline underline-offset-8`}
                                >
                                    {user.name}
                                </p>
                                <div className="flex gap-2 items-center">
                                    <FaPen
                                        cursor={'pointer'}
                                        color="white"
                                        size={24}
                                        onClick={() => {
                                            choseUser(user);
                                            openEdit();
                                        }}
                                    />
                                    <MdOutlineDisabledByDefault
                                        cursor={'pointer'}
                                        color="red"
                                        size={36}
                                        onClick={() => {
                                            choseUser(user);
                                            openDel();
                                        }}
                                    />
                                </div>
                            </div>
                            {!isCoop && (
                                <div className="h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                                    <div className="flex flex-col w-full gap-4">
                                        <div className={`text-slate-200`}>
                                            <h3>Счета:</h3>
                                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                                {['с1', 'c2', 'c3'].map((i, index) => (
                                                    <Link href={`/accounts/${index}`}>{i}</Link>
                                                ))}
                                            </div>
                                        </div>
                                        <div className={`text-[rgb(255,0,255)]`}>
                                            <h3>Кредиты:</h3>
                                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                                {['с1', 'c2', 'c3'].map((i, index) => (
                                                    <Link href={`/credits/${index}`}>{i}</Link>
                                                ))}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </motion.li>
                    ))}
                </motion.ul>
            </AnimatePresence>
        );
};

export default UserGrid;
