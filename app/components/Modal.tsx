import { Dialog, DialogPanelProps } from '@headlessui/react';
import { FC } from 'react';
import { ModalProps } from '../config';
import { motion } from 'framer-motion';

type ModalType = ModalProps & DialogPanelProps<any>;

const Modal: FC<ModalType> = ({ children, isOpen, onClose, ...rest }) => {
    return (
        <Dialog open={isOpen} onClose={onClose} className="relative z-50">
            <div className="fixed inset-0 bg-black/80" aria-hidden="true" />
            <motion.div
                initial={{ scale: 0.5 }}
                animate={{ scale: 1 }}
                className="fixed inset-0 w-screen flex items-center justify-center "
            >
                <Dialog.Panel {...rest}>{children}</Dialog.Panel>
            </motion.div>
        </Dialog>
    );
};

export default Modal;
