import Modal from '@/app/components/Modal';
import { ModalProps, IdName } from '@/app/config';
import { FC } from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';

const EditCredModal: FC<ModalProps & { credit: (IdName & { percent: number }) | undefined }> = ({
    isOpen,
    onClose,
    credit,
}) => {
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

    if (!credit) {
        return null;
    }

    const onEditClose = () => {
        reset();
        onClose();
    };

    return (
        <Modal
            isOpen={isOpen}
            onClose={onEditClose}
            style={{
                backgroundColor: 'rgb(var(--background-end-rgb))',
                color: 'white',
            }}
            className="p-4 rounded-xl text-2xl text-center"
        >
            <div className="p-12 rounded-xl text-2xl text-center">
                <h1 className="text-3xl font-bold underline underline-offset-8 mb-6">Изменить кредит</h1>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="p-4 flex flex-col">
                        <p>Введите название</p>
                        <input
                            defaultValue={credit.name}
                            {...register('name', { required: true })}
                            className="text-black"
                        />
                        {errors.percent && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>
                    <div className="p-4 flex flex-col">
                        <p>Задайте % ставку</p>
                        <input
                            defaultValue={credit.percent}
                            type='number'
                            {...register('percent', { required: true })}
                            className="text-black"
                        />
                        {errors.percent && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>

                    <div className="flex gap-4 mt-4 justify-center">
                        <button type="submit" className="p-4 bg-gray-500 rounded-full">
                            Создать
                        </button>
                        <button className="p-4 bg-black rounded-full" onClick={onEditClose}>
                            Отмена
                        </button>
                    </div>
                </form>
            </div>
        </Modal>
    );
};

export default EditCredModal;
