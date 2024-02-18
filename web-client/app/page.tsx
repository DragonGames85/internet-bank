'use client';

import { useEffect, useState } from 'react';
import { MdAddCircleOutline } from 'react-icons/md';
import AddModal from './components/AddModal';
import DeleteModal from './components/DeleteModal';
import EditModal from './components/EditModal';
import UserGrid from './components/UserGrid';
import { IdName } from './config';

export default function Home() {
    const people = [
        { id: 1, name: 'Данил' },
        { id: 2, name: 'Никита' },
        { id: 3, name: 'Ярик' },
        { id: 4, name: 'Олег' },
        { id: 5, name: 'Соня' },
        { id: 6, name: 'Вячеслав' },
    ];

    const coops = [
        { id: 1, name: 'Диана' },
        { id: 2, name: 'Костя' },
        { id: 3, name: 'Денис' },
        { id: 4, name: 'Митя' },
        { id: 5, name: 'Павел' },
        { id: 6, name: 'Эрик' },
    ];

    const [isCoop, setIsCoop] = useState(false);
    const [isAddOpen, openAdd] = useState(false);
    const [isDelOpen, openDel] = useState(false);
    const [isEditOpen, openEdit] = useState(false);
    const [user, choseUser] = useState<IdName>();

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
            <UserGrid
                array={isCoop ? coops : people}
                choseUser={choseUser}
                isCoop={isCoop}
                openDel={() => openDel(true)}
                openEdit={() => openEdit(true)}
            />
        </>
    );
}
