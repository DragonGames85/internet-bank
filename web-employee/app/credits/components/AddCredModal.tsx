'use client';
import { api } from '@/app/api';
import { Credit } from '@/app/api/types';
import Modal from '@/app/components/Modal';
import { ModalProps } from '@/app/config';
import { useModalFetch } from '@/app/hooks/useModalFetch';
import { Dialog } from '@headlessui/react';

const AddCredModal = ({ isOpen, onClose }: ModalProps) => {
    type Inputs = Omit<Credit, 'id'>;

    // as keyof Inputs
    const numberArray: (keyof Inputs)[] = [
        'maxCreditSum',
        'minCreditSum',
        'minRepaymentPeriod',
        'maxRepaymentPeriod',
        'paymentType',
        'pennyPercent',
        'rateType',
    ];

    const { errors, handleSubmit, mutate, onModalClose, onSubmit, register } = useModalFetch<Inputs>(async data => {
        try {
            await api.credits.post(data);
            mutate('/api/credits');
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
                Добавить кредит
            </Dialog.Title>

            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="p-4 flex flex-col">
                    <p>Введите название</p>
                    <input {...register('name', { required: true })} className="text-black" />
                    {errors.name && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Задайте % ставку</p>
                    <input type="number" {...register('percent', { required: true })} className="text-black" />
                    {errors.percent && <span className="text-red-600">Это поле обязательно!</span>}
                </div>

                {numberArray.map(inp => {
                    return (
                        <div className="p-4 flex flex-col">
                            <p>Введите {inp}</p>
                            <input type="number" {...register(inp, { required: true })} className="text-black" />
                            {errors[inp] && <span className="text-red-600">Это поле обязательно!</span>}
                        </div>
                    );
                })}

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

export default AddCredModal;
