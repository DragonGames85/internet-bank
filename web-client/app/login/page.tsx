'use client';
import { useForm, SubmitHandler } from 'react-hook-form';

const Login = () => {
    type Inputs = {
        name: string;
        password: string;
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
        <div className="flex w-full justify-center">
            <form
                onSubmit={handleSubmit(onSubmit)}
                style={{
                    backgroundColor: 'rgb(var(--background-end-rgb))',
                }}
                className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8"
            >
                <div className="p-4 flex flex-col">
                    <p>Введите имя</p>
                    <input {...register('name', { required: true })} className="text-black" />
                    {errors.password && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите пароль</p>
                    <input {...register('password', { required: true })} className="text-black" />
                    {errors.password && <span className="text-red-600">Это поле обязательно!</span>}
                </div>

                <button type="submit" className="p-4 bg-gray-500 rounded-full mt-4">
                    Войти
                </button>
            </form>
        </div>
    );
};

export default Login;
