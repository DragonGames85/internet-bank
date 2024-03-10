'use client';
import { useForm, SubmitHandler } from 'react-hook-form';
import { api } from '../api';
import axios from 'axios';

const Login = () => {
    type LoginType = {
        login: string;
        password: string;
    };
    type RegisterType = {
        login: string;
        password: string;
        name: string;
        confirmPassword: string;
    };

    const {
        register,
        handleSubmit,
        formState: { errors },
        reset,
    } = useForm<LoginType>();

    const {
        register: register2,
        handleSubmit: handleSubmit2,
        formState: { errors: errors2 },
        reset: reset2,
    } = useForm<RegisterType>();

    const onLogin: SubmitHandler<LoginType> = async data => {
        const { token, userId } = await api.auth.login(data);
        localStorage.setItem('token', token);
        localStorage.setItem('userId', userId);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        window.location.reload();
    };

    const onRegister: SubmitHandler<RegisterType> = async data => {
        const { token, userId } = await api.auth.register(data);
        localStorage.setItem('token', token);
        localStorage.setItem('userId', userId);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        window.location.reload();
    };

    return (
        <div className="flex gap-2 items-center justify-between w-full">
            <div className="flex flex-col w-full justify-center items-center gap-2">
                <h1 className="text-2xl">Логин</h1>
                <form
                    onSubmit={handleSubmit(onLogin)}
                    style={{
                        backgroundColor: 'rgb(var(--background-end-rgb))',
                    }}
                    className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8"
                >
                    <div className="p-4 flex flex-col">
                        <p>Введите логин</p>
                        <input {...register('login', { required: true })} className="text-black" />
                        {errors.login && <span className="text-red-600">Это поле обязательно!</span>}
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
            <div className="flex flex-col w-full justify-center items-center gap-2">
                <h1 className="text-2xl">Регистрация</h1>
                <form
                    onSubmit={handleSubmit2(onRegister)}
                    style={{
                        backgroundColor: 'rgb(var(--background-end-rgb))',
                    }}
                    className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8"
                >
                    <div className="p-4 flex flex-col">
                        <p>Введите логин</p>
                        <input {...register2('login', { required: true })} className="text-black" />
                        {errors2.login && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>
                    <div className="p-4 flex flex-col">
                        <p>Введите пароль</p>
                        <input {...register2('password', { required: true })} className="text-black" />
                        {errors2.password && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>
                    <div className="p-4 flex flex-col">
                        <p>Введите имя</p>
                        <input {...register2('name', { required: true })} className="text-black" />
                        {errors2.name && <span className="text-red-600">Это поле обязательно!</span>}
                    </div>
                    <button type="submit" className="p-4 bg-gray-500 rounded-full mt-4">
                        Войти
                    </button>
                </form>
            </div>
        </div>
    );
};

export default Login;
