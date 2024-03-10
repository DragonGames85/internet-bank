import { FC } from 'react';
import { api } from '../api';
import { User } from '../api/types';
import { ModalProps } from '../config';
import { useModalFetch } from '../hooks/useModalFetch';
import Modal from './Modal';

const EditModal: FC<ModalProps & { isCoop: boolean; user: User | undefined }> = ({ isOpen, onClose, isCoop, user }) => {
    const titleText = isCoop ? 'сотрудника' : 'клиента';

    type Inputs = {
        name: string;
        password: string;
    };

    if (!user) {
        return null;
    }

    const { errors, handleSubmit, mutate, onModalClose, onSubmit, register } = useModalFetch<Inputs>(async data => {
        try {
            await api.users.put(user?.id, { ...user, name: data.name });
            mutate('/api/users', async (prev: any) => {
                return prev.map((u: any) => {
                    if (u.id === user?.id) {
                        return { ...u, name: data.name };
                    }
                    return u;
                });
            });
        } catch (error) {}
    }, onClose);

    return (
        <Modal
            isOpen={isOpen}
            onClose={onModalClose}
            style={{
                backgroundColor: 'rgb(var(--background-end-rgb))',
                color: 'white',
            }}
            className="p-4 rounded-xl text-2xl text-center"
        >
            <div className="p-12 rounded-xl text-2xl text-center">
                <h1 className="text-3xl font-bold underline underline-offset-8 mb-6">Изменить {titleText}</h1>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="p-4 flex flex-col">
                        <p>Введите имя</p>
                        <input
                            defaultValue={user.name}
                            {...register('name', { required: true })}
                            className="text-black"
                        />
                        {errors.name && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>
                    <div className="p-4 flex flex-col">
                        <p>Задайте пароль</p>
                        <input {...register('password', { required: true })} className="text-black" />
                        {errors.password && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>

                    <div className="flex gap-4 mt-4 justify-center">
                        <button type="submit" className="p-4 bg-gray-500 rounded-full">
                            Изменить
                        </button>
                        <button className="p-4 bg-black rounded-full" onClick={onModalClose}>
                            Отмена
                        </button>
                    </div>
                </form>
            </div>
        </Modal>
    );
};

export default EditModal;
