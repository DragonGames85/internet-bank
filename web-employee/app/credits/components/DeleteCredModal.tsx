import { api } from '@/app/api';
import { Credit } from '@/app/api/types';
import Modal from '@/app/components/Modal';
import { ModalProps } from '@/app/config';
import { FC } from 'react';
import { useSWRConfig } from 'swr';

const DeleteCredModal: FC<ModalProps & { credit: Credit | undefined }> = ({ isOpen, onClose, credit }) => {
    const { mutate } = useSWRConfig();

    if (!credit) {
        return null;
    }

    const handleDelete = async () => {
        try {
            await api.credits.delete(credit.id);
            mutate('/api/credits', async (prev: any) => {
                return prev.filter((c: any) => c.id !== credit.id);
            });
        } catch (error) {}
        onClose();
    };

    return (
        <Modal
            isOpen={isOpen}
            onClose={onClose}
            style={{
                backgroundColor: 'rgb(var(--background-end-rgb))',
                color: 'white',
            }}
            className="p-4 rounded-xl text-2xl text-center"
        >
            <div className="p-12 rounded-xl text-2xl text-center">
                <h1 className="text-3xl font-bold underline underline-offset-8 mb-6">Удалить кредит</h1>
                <p>Вы уверены, что хотите удалить кредит {credit.name}?</p>
                <div className="flex gap-4 mt-4 justify-center">
                    <button onClick={handleDelete} className="p-4 bg-gray-500 rounded-full">
                        Удалить
                    </button>
                    <button className="p-4 bg-black rounded-full" onClick={onClose}>
                        Отмена
                    </button>
                </div>
            </div>
        </Modal>
    );
};

export default DeleteCredModal;
