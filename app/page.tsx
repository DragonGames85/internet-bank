'use client';

import { useEffect, useState } from 'react';
import { MdAddCircleOutline } from 'react-icons/md';
import useSWR from 'swr';
import { api } from './api';
import { User } from './api/types';
import AddModal from './components/AddModal';
import DeleteModal from './components/DeleteModal';
import EditModal from './components/EditModal';
import UserGrid from './components/UserGrid';

export default function Home() {
    const { data: users } = useSWR<User[]>('/api/users', api.users.getAll);

    const [isCoop, setIsCoop] = useState(false);
    const [isAddOpen, openAdd] = useState(false);
    const [isDelOpen, openDel] = useState(false);
    const [isEditOpen, openEdit] = useState(false);
    const [user, choseUser] = useState<User>();

    useEffect(() => {
        if (isCoop) {
            document.documentElement.style.setProperty('--background-end-rgb', '0 0 120');
        } else {
            document.documentElement.style.setProperty('--background-end-rgb', '0 80 0');
        }
    }, [isCoop]);

    return (
        <>
            <AddModal isOpen={isAddOpen} onClose={() => openAdd(false)} isCoop={isCoop} />
            <EditModal
                isOpen={isEditOpen}
                onClose={() => {
                    openEdit(false);
                    choseUser(undefined);
                }}
                isCoop={isCoop}
                user={user}
            />
            <DeleteModal
                isOpen={isDelOpen}
                onClose={() => {
                    openDel(false);
                    choseUser(undefined);
                }}
                isCoop={isCoop}
                user={user}
            />
            <div
                className={
                    'w-full py-8 text-3xl flex flex-col sm:flex-row items-center justify-center gap-2 relative sm:gap-2 md:gap-6 lg:gap-12 xl:gap-16 2xl:gap-24'
                }
            >
                <p
                    style={{ cursor: 'pointer' }}
                    className={!isCoop ? 'border-green-400  border-2 rounded-full p-2 bg-green-900  ' : 'p-2'}
                    onClick={() => {
                        setIsCoop(false);
                    }}
                >
                    Клиенты
                </p>
                <p
                    style={{ cursor: 'pointer', color: 'cyan' }}
                    className={isCoop ? 'border-blue-400 border-2 rounded-full p-2 bg-blue-600' : 'p-2'}
                    onClick={() => {
                        setIsCoop(true);
                    }}
                >
                    Сотрудники
                </p>
                <button
                    onClick={() => openAdd(true)}
                    style={{
                        color: isCoop ? 'rgb(0, 255, 255)' : 'rgb(0, 255,0)',
                    }}
                    className="lg:absolute lg:right-0 border-2 rounded-2xl p-2 flex gap-2 items-center bg-gray-900"
                >
                    Добавить
                    <MdAddCircleOutline />
                </button>
            </div>
            {users && users.length && (
                <UserGrid
                    array={users.filter(user => user.role == (isCoop ? 'COOP' : 'USER'))}
                    choseUser={choseUser}
                    isCoop={isCoop}
                    openDel={() => openDel(true)}
                    openEdit={() => openEdit(true)}
                />
            )}
        </>
    );
}
