'use client';
import Modal from '@/app/components/Modal';
import { ModalProps } from '@/app/config';
import { Dialog } from '@headlessui/react';
import { useForm, SubmitHandler } from 'react-hook-form';

const AddCredModal = ({ isOpen, onClose }: ModalProps) => {
    type Inputs = {
        name: string;
        percent: string;
    };
    const {
        register,
        handleSubmit,
        formState: { errors },
        reset,
    } = useForm<Inputs>();
    const onSubmit: SubmitHandler<Inputs> = data => {
        console.log(data);
        reset();
    };

    return (
        <Modal
            onClose={onClose}
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
                    {errors.percent && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Задайте % ставку</p>
                    <input {...register('percent', { required: true })} className="text-black" />
                    {errors.percent && <span className="text-red-600">Это поле обязательно!</span>}
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

export default AddCredModal;
