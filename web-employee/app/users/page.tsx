'use client';

import { useState } from 'react';
import { FaKey } from 'react-icons/fa';
import { MdAddCircleOutline } from 'react-icons/md';
import { useSWRConfig } from 'swr';
import { api } from '../api';
import { User } from '../api/types';
import { useBankFetch } from '../hooks/useBankFetch';
import AddModal from './components/AddModal';
import InfoModal from './components/InfoModal';
import UserGrid from './components/UserGrid';

const Users = () => {
    const [isCoop, setIsCoop] = useState(false);
    const [isAddOpen, openAdd] = useState(false);
    const [isInfoOpen, openInfo] = useState(false);
    const [user, choseUser] = useState<User>();

    const mockedUsers: User[] = [
        {
            id: '1',
            isBanned: false,
            name: 'Иван',
            role: 'Customer',
        },
        {
            id: '2',
            isBanned: false,
            name: 'Григорий',
            role: 'Customer',
        },
        {
            id: '3',
            isBanned: true,
            name: 'Коля',
            role: 'Customer',
        },
        {
            id: '11',
            isBanned: false,
            name: 'Данил',
            role: 'Employee',
        },
        {
            id: '22',
            isBanned: false,
            name: 'Ярик',
            role: 'Employee',
        },
        {
            id: '33',
            isBanned: true,
            name: 'Илья',
            role: 'Employee',
        },
    ];

    const { result: users, error, mock } = useBankFetch<User[]>('/api/users', api.users.getAll, mockedUsers);

    const { mutate } = useSWRConfig();
    const handleDelete = async () => {
        if (!user) return;
        const titleText = isCoop ? 'сотрудника' : 'клиента';
        const isYes = confirm('Вы уверены, что хотите удалить ' + titleText + ' ' + user.name + '?');
        if (isYes)
            try {
                await api.users.delete(user.id);
                mutate('/api/users', async (prev: any) => {
                    return prev.filter((u: any) => u.id !== user.id);
                });
            } catch (error: any) {
                alert(error.message);
            }
    };

    return (
        <>
            <AddModal isOpen={isAddOpen} onClose={() => openAdd(false)} isCoop={isCoop} />
            <InfoModal
                isOpen={isInfoOpen}
                onClose={() => {
                    openInfo(false);
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
                <button
                    onClick={() => mock(prev => !prev)}
                    style={{
                        color: isCoop ? 'rgb(0, 255, 255)' : 'rgb(0, 255,0)',
                    }}
                    className="lg:absolute lg:left-0 border-2 rounded-2xl p-2 flex gap-2 items-center bg-gray-900"
                >
                    Мок
                    <FaKey color="fuchsia" />
                </button>
                <p
                    style={{ cursor: 'pointer' }}
                    className={!isCoop ? 'border-green-400  border-2 rounded-full p-2 bg-green-900 text-white' : 'p-2'}
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
            {error && <p>Ошибка сервера: {error.message}</p>}
            {users && users.length && (
                <UserGrid
                    users={users.filter(user => user.role == (isCoop ? 'Employee' : 'Customer'))}
                    choseUser={choseUser}
                    isCoop={isCoop}
                    openEdit={() => openInfo(true)}
                    deleteUser={handleDelete}
                />
            )}
        </>
    );
};

export default Users;
