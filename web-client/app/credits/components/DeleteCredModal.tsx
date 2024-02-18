import Modal from '@/app/components/Modal';
import { ModalProps, IdName } from '@/app/config';
import { FC } from 'react';

const DeleteCredModal: FC<ModalProps & { credit: (IdName & { percent: number }) | undefined }> = ({
    isOpen,
    onClose,
    credit,
}) => {
    if (!credit) {
        return null;
    }

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
                    <button onClick={onClose} className="p-4 bg-gray-500 rounded-full">
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
