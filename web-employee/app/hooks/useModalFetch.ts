import { FieldValues, SubmitHandler, useForm } from 'react-hook-form';
import { useSWRConfig } from 'swr';

export const useModalFetch = <T extends FieldValues>(func: (data: T) => void, onClose: () => void) => {
    const { mutate } = useSWRConfig();

    const {
        register,
        handleSubmit,
        formState: { errors },
        reset,
    } = useForm<T>();

    const onModalClose = () => {
        reset();
        onClose();
    };

    const onSubmit: SubmitHandler<T> = async data => {
        func(data);
        onModalClose();
    };

    return { register, handleSubmit, errors, onModalClose, onSubmit, mutate };
};
