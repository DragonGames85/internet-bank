import { FC } from 'react';
import { api } from '../../api';
import { User } from '../../api/types';
import { ModalProps } from '../../config';
import Modal from '../../components/Modal';
import useSWR from 'swr';
import Link from 'next/link';

const EditModal: FC<ModalProps & { isCoop: boolean; user: User | undefined }> = ({ isOpen, onClose, isCoop, user }) => {
    if (!user || isCoop) {
        return null;
    }

    const { data: accounts } = useSWR(`/api/accounts/${user.id}`, () => api.accounts.userAccounts(user.id));
    const { data: credits } = useSWR(`/api/credits/${user.id}`, () => api.credits.getUserCredits(user.id));
    const { data: expCredits } = useSWR(`/api/credits/${user.id}`, () => api.credits.expired(user.id));

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
                <div className="h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                    <div className="flex flex-col w-full gap-4">
                        <div className={`text-slate-200`}>
                            <h3>Счета:</h3>
                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                {accounts?.map(acc => (
                                    <Link key={acc.id} href={`/accounts/${acc.id}`}>
                                        {acc.id}
                                    </Link>
                                ))}
                            </div>
                        </div>
                        <div className={`text-[rgb(255,0,255)]`}>
                            <h3>Кредиты:</h3>
                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                {credits?.map(cred => (
                                    <Link key={cred.id} href={`/credits?id=${cred.id}`}>
                                        {cred.id}
                                    </Link>
                                ))}
                            </div>
                        </div>
                        <div className={`text-[rgb(80,31,80)]`}>
                            <h3>Проср. кредиты:</h3>
                            <div className="flex gap-5 underline underline-offset-8 flex-wrap">
                                {expCredits?.map(cred => (
                                    <Link key={cred.id} href={`/credits?id=${cred.id}`}>
                                        {cred.id}
                                    </Link>
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </Modal>
    );
};

export default EditModal;
