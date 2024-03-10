'use client';
import { api } from '@/app/api';
import Modal from '@/app/components/Modal';
import { ModalProps } from '@/app/config';
import { useModalFetch } from '@/app/hooks/useModalFetch';
import { Dialog } from '@headlessui/react';

const AddAccModal = ({ isOpen, onClose }: ModalProps) => {
    type Inputs = {
        type: number;
        currencyName: string;
    };

    const { errors, handleSubmit, mutate, onModalClose, onSubmit, register } = useModalFetch<Inputs>(async data => {
        try {
            await api.accounts.post(data);
            mutate('/api/accounts');
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
            <Dialog.Title className={'text-3xl font-bold underline underline-offset-8 mb-6'}>Открыть счёт</Dialog.Title>

            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="p-4 flex flex-col">
                    <p>Введите тип</p>
                    <select {...register('type', { required: true })} className="text-black">
                        <option value={0}>Дебетовая</option>
                        <option value={1}>Кредитовая</option>
                    </select>
                    {errors.type && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите валюта</p>
                    <select {...register('currencyName', { required: true })} className="text-black">
                        <option value={'RUB'}>Рубли</option>
                        <option value={'USD'}>Доллары</option>
                    </select>
                    {errors.type && <span className="text-red-600">Это поле обязательно!</span>}
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

export default AddAccModal;
