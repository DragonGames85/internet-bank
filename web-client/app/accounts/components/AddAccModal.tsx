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
        const userId = JSON.parse(localStorage.getItem('user') ?? '').userId;
        try {
            await api.accounts.post({ ...data, userId });
            mutate('/api/accounts');
        } catch (error) {}
    }, onClose);

    return (
        <Modal onClose={onModalClose} isOpen={isOpen} className="p-12 rounded-xl text-2xl text-center bg-bgColor">
            <Dialog.Title className={'text-3xl font-bold underline underline-offset-8 mb-6'}>Открыть счёт</Dialog.Title>

            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="p-4 flex flex-col">
                    <p>Введите тип</p>
                    <select {...register('type', { required: true })}>
                        <option value={0}>Дебетовая</option>
                        <option value={1}>Кредитовая</option>
                    </select>
                    {errors.type && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите валюта</p>
                    <select {...register('currencyName', { required: true })}>
                        {[
                            'RUB',
                            'AUD',
                            'AZN',
                            'GBP',
                            'AMD',
                            'BYN',
                            'BGN',
                            'BRL',
                            'HUF',
                            'VND',
                            'HKD',
                            'GEL',
                            'DKK',
                            'AED',
                            'USD',
                            'EUR',
                            'EGP',
                            'INR',
                            'IDR',
                            'KZT',
                            'CAD',
                            'QAR',
                            'KGS',
                            'CNY',
                            'MDL',
                            'NZD',
                            'NOK',
                            'PLN',
                            'RON',
                            'XDR',
                            'SGD',
                            'TJS',
                            'THB',
                            'TRY',
                            'TMT',
                            'UZS',
                            'UAH',
                            'CZK',
                            'SEK',
                            'CHF',
                            'RSD',
                            'ZAR',
                            'KRW',
                            'JPY',
                        ].map(val => (
                            <option key={val} value={val}>
                                {val}
                            </option>
                        ))}
                    </select>
                    {errors.type && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="flex gap-4 mt-4 justify-center">
                    <button type="submit" className="p-4 bg-bgColor2 rounded-full">
                        Создать
                    </button>
                    <button className="p-4 bg-black rounded-full bg-bgColor3" onClick={onClose}>
                        Отмена
                    </button>
                </div>
            </form>
        </Modal>
    );
};

export default AddAccModal;
