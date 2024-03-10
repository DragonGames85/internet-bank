'use client';
import { Dialog } from '@headlessui/react';
import { api } from '../api';
import { ModalProps } from '../config';
import { useModalFetch } from '../hooks/useModalFetch';
import Modal from './Modal';

const AddModal = ({ isOpen, onClose, isCoop }: ModalProps & { isCoop: boolean }) => {
    const titleText = isCoop ? 'сотрудника' : 'клиента';
    type Inputs = {
        name: string;
        login: string;
        password: string;
    };

    const { errors, handleSubmit, mutate, onModalClose, onSubmit, register } = useModalFetch<Inputs>(async data => {
        try {
            await api.users.post({
                ...data,
                role: isCoop ? 'Employee' : 'Customer',
            });
            mutate('/api/users');
        } catch (error) {}
    }, onClose);

    return (
        <Modal
            onClose={onModalClose}
            isOpen={isOpen}
            style={{
                backgroundColor: 'rgb(var(--background-end-rgb))',
                color: 'white',
            }}
            className="p-12 rounded-xl text-2xl text-center"
        >
            <Dialog.Title className={'text-3xl font-bold underline underline-offset-8 mb-6'}>
                Добавить {titleText}
            </Dialog.Title>

            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="p-4 flex flex-col">
                    <p>Введите имя</p>
                    <input {...register('name', { required: true })} className="text-black" />
                    {errors.name && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите логин</p>
                    <input {...register('login', { required: true })} className="text-black" />
                    {errors.login && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Задайте пароль</p>
                    <input {...register('password', { required: true })} className="text-black" />
                    {errors.password && <span className="text-red-600">Это поле обязательно!</span>}
                </div>

                <div className="flex gap-4 mt-4 justify-center">
                    <button type="submit" className="p-4 bg-gray-500 rounded-full">
                        Создать
                    </button>
                    <button className="p-4 bg-black rounded-full" onClick={onClose}>
                        Отмена
                    </button>
                </div>
            </form>
        </Modal>
    );
};

export default AddModal;

//     <div className="fixed inset-0 bg-black/80" aria-hidden="true" />
//     <div className="fixed inset-0 flex w-screen items-center justify-center p-4 text-center">
//         <Dialog.Panel
//         >
//             <Dialog.Title className={'text-3xl font-bold underline underline-offset-8 mb-6'}>
//                 Добавить {titleText}
//             </Dialog.Title>

//             <form onSubmit={handleSubmit(onSubmit)}>
//                 <div className="p-4 flex flex-col">
//                     <p>Введите ФИО</p>
//                     <input {...register('name', { required: true })} className="text-black" />
//                     {errors.password && <span className="text-red-600">This field is required</span>}
//                 </div>
//                 <div className="p-4 flex flex-col">
//                     <p>Задайте пароль</p>
//                     <input {...register('password', { required: true })} className="text-black" />
//                     {errors.password && <span className="text-red-600">This field is required</span>}
//                 </div>

//                 <div className="flex gap-4 mt-4 justify-center">
//                     <button type="submit" className="p-4 bg-gray-500 rounded-full">
//                         Создать
//                     </button>
//                     <button className="p-4 bg-black rounded-full" onClick={() => setIsOpen(false)}>
//                         Отмена
//                     </button>
//                 </div>
//             </form>
//         </Dialog.Panel>
//     </div>
