import { AnimatePresence, motion } from 'framer-motion';
import Link from 'next/link';
import { FC } from 'react';
import { FaPen } from 'react-icons/fa';
import { MdOutlineDisabledByDefault } from 'react-icons/md';
import { User } from '../api/types';
import { adaptiveClass, animationVariants } from '../config';
import { MdBlock } from 'react-icons/md';
import { api } from '../api';
import { useSWRConfig } from 'swr';
interface IUserGrid {
    isCoop: boolean;
    array: User[];
    choseUser: (user: User) => void;
    openEdit: () => void;
    openDel: () => void;
}

const UserGrid: FC<IUserGrid> = ({ array, isCoop, choseUser, openDel, openEdit }) => {
    let colsClass = '';
    colsClass = adaptiveClass('grid-cols', [1, 3, 1, 3, 1, 3]);

    const bgColor = isCoop ? 'bg-blue-950' : 'bg-green-950';

    const { mutate } = useSWRConfig();

    if (colsClass)
        return (
            <AnimatePresence>
                <motion.ul
                    className={`grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5`}
                    key={isCoop ? 'coops' : 'people'}
                    variants={animationVariants.container}
                    initial="hidden"
                    animate="visible"
                >
                    {array.length === 0 && (
                        <motion.li
                            key={0}
                            variants={animationVariants.item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 
                        ${bgColor}
                        h-auto gap-1`}
                        >
                            <p className="text-3xl">Нет пользователей</p>
                        </motion.li>
                    )}
                    {array.map((user, index) => (
                        <motion.li
                            key={index}
                            variants={animationVariants.item}
                            className={`flex flex-col items-start border-[1px] rounded-3xl p-6 relative 
                        ${user.isBanned ? '' : bgColor}
                        h-auto gap-1`}
                        >
                            {user.isBanned && (
                                <>
                                    <div className="absolute w-full h-full bg-red-900 opacity-60 left-0 top-0 border-[1px] rounded-3xl z-[1]" />
                                    {user.role != 'COOP' && (
                                        <MdBlock
                                            color="red"
                                            className="absolute h-full top-0 md:top-auto left-0 right-0 ml-auto mr-auto size-[150px] md:size-[208px]"
                                        />
                                    )}
                                </>
                            )}
                            <div key={index} className="flex w-full text-3xl justify-between">
                                <p
                                    style={{
                                        color: isCoop ? 'rgb(0, 255, 255)' : 'rgb(0, 255,0)',
                                    }}
                                    className={`self-center`}
                                >
                                    {user.name}
                                </p>
                                <div className="flex gap-2 items-center z-[2]">
                                    {!user.isBanned && (
                                        <>
                                            <FaPen
                                                cursor={'pointer'}
                                                color="white"
                                                size={24}
                                                onClick={() => {
                                                    choseUser(user);
                                                    openEdit();
                                                }}
                                            />
                                        </>
                                    )}
                                    <MdOutlineDisabledByDefault
                                        cursor={'pointer'}
                                        color="red"
                                        size={36}
                                        onClick={() => {
                                            choseUser(user);
                                            openDel();
                                        }}
                                    />
                                    <MdBlock
                                        cursor={'pointer'}
                                        color={user.isBanned ? 'red' : 'maroon'}
                                        size={36}
                                        onClick={async () => {
                                            await api.users.ban(user.id, { ...user, isBanned: !user.isBanned });
                                            mutate('/api/users');
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
                                                {user.accounts?.map(acc => (
                                                    <Link key={acc} href={`/accounts/${acc}`}>
                                                        {acc}
                                                    </Link>
                                                ))}
                                            </div>
                                        </div>
                                        <div className={`text-[rgb(255,0,255)]`}>
                                            <h3>Кредиты:</h3>
                                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                                {user.credits?.map(cred => (
                                                    <Link key={cred} href={`/credits?id=${cred}`}>
                                                        {cred}
                                                    </Link>
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
