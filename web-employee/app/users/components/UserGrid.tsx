import { AnimatePresence, motion } from 'framer-motion';
import { FC } from 'react';
import { MdBlock, MdOutlineDisabledByDefault } from 'react-icons/md';
import { RiFolderInfoLine } from 'react-icons/ri';
import { useSWRConfig } from 'swr';
import { api } from '../../api';
import { User } from '../../api/types';
import { animationVariants } from '../../config';
interface IUserGrid {
    isCoop: boolean;
    users: User[];
    choseUser: (user: User) => void;
    openEdit: () => void;
    deleteUser: () => void;
}

const UserGrid: FC<IUserGrid> = ({ users, isCoop, choseUser, deleteUser, openEdit }) => {
    const bgColor = isCoop ? 'bg-blue-950' : 'bg-green-950';

    const { mutate } = useSWRConfig();

    return (
        <AnimatePresence>
            <motion.ul
                className={`grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-2 2xl:grid-cols-3 gap-12 pt-5`}
                key={isCoop ? 'coops' : 'people'}
                variants={animationVariants.container}
                initial="hidden"
                animate="visible"
            >
                {users.length === 0 && (
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
                {users.map((user, index) => (
                    <motion.li
                        key={index}
                        variants={animationVariants.item}
                        className={`flex flex-col items-start border-[1px] rounded-3xl p-6 relative 
                        ${user.isBanned ? 'bg-red-900' : bgColor}
                        h-auto gap-1`}
                    >
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
                                {!user.isBanned && !isCoop && (
                                    <>
                                        <RiFolderInfoLine
                                            cursor={'pointer'}
                                            color="white"
                                            size={32}
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
                                        deleteUser();
                                    }}
                                />
                                <MdBlock
                                    cursor={'pointer'}
                                    color={user.isBanned ? 'red' : 'maroon'}
                                    size={36}
                                    onClick={async () => {
                                        await api.users.ban(user.id);
                                        mutate('/api/users');
                                    }}
                                />
                            </div>
                        </div>
                        <p>Кредитный рейтинг - {user.creditRating ?? '??'}</p>
                    </motion.li>
                ))}
            </motion.ul>
        </AnimatePresence>
    );
};

export default UserGrid;
