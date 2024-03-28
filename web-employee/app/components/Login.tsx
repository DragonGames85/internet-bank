'use client';
import axios from 'axios';
import { signIn } from 'next-auth/react';
import { SubmitHandler, useForm } from 'react-hook-form';
import { api } from '../api';

const Login = () => {
    type RegisterType = {
        login: string;
        password: string;
        name: string;
    };

    const {
        register,
        handleSubmit,
        formState: { errors },
        reset,
    } = useForm<RegisterType>();

    const onRegister: SubmitHandler<RegisterType> = async data => {
        const { token, userId } = await api.auth.register(data);
        localStorage.setItem('token', token);
        localStorage.setItem('userId', userId);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        window.location.reload();
    };

    return (
        <div className="flex flex-col w-full justify-center items-center gap-2">
            <h1 className="text-3xl font-medium">Регистрация</h1>
            <form
                onSubmit={handleSubmit(onRegister)}
                className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8 bg-bgColor2 dark:bg-bgColor3Dark"
            >
                <div className="p-4 flex flex-col">
                    <p>Введите логин</p>
                    <input {...register('login', { required: true })} />
                    {errors.login && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите пароль</p>
                    <input {...register('password', { required: true })} />
                    {errors.password && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <div className="p-4 flex flex-col">
                    <p>Введите имя</p>
                    <input {...register('name', { required: true })} />
                    {errors.name && <span className="text-red-600">Это поле обязательно!</span>}
                </div>
                <button type="submit" className={`p-4 bg-bgColor rounded-full mt-4`}>
                    Зарегистрироваться
                </button>
            </form>
            <h2 className="mt-16 text-3xl">ИЛИ</h2>
            <button
                className="border-2 rounded-full bg-bgColor2 dark:bg-bgColor3Dark text-3xl text-center self-center p-4 px-16 mt-16"
                onClick={() => signIn()}
            >
                ВОЙТИ
            </button>
        </div>
    );
};

export default Login;
