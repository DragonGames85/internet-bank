'use client';
import axios from 'axios';
import { useRouter } from 'next/navigation';
import { SubmitHandler, useForm } from 'react-hook-form';
import ClockTime from './helpers/ClockTime';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './helpers/useLocalStorage';

export default function Home(props: { searchParams: { home: string } }) {
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
    } = useForm<LoginType>();

    const {
        register: register2,
        handleSubmit: handleSubmit2,
        formState: { errors: errors2 },
    } = useForm<RegisterType>();

    const navigate = useRouter();

    const onLogin: SubmitHandler<LoginType> = async data => {
        try {
            const { token } = await axios
                .post<{ token: string }>('https://bayanshonhodoev.ru/auth/api/auth/login', data)
                .then(res => res.data);
            // localStorage.setItem('token', token);
            // axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            navigate.push('http://' + (props.searchParams.home ?? 'localhost:3001') + `?token=${token}`);
        } catch (error) {
            console.log(error);
        }
    };

    const onRegister: SubmitHandler<RegisterType> = async data => {
        try {
            const { token } = await axios
                .post<{ token: string }>('https://bayanshonhodoev.ru/auth/api/auth/register', data)
                .then(res => res.data);
            // localStorage.setItem('token', token);
            // axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            navigate.push('http://' + (props.searchParams.home ?? 'localhost:3001') + `?token=${token}`);
        } catch (error) {
            console.log(error);
        }
    };

    const [token, _] = useLocalStorage('token', '');
    const tokenData = token ? parseJwt(token) : '';
    const exp = new Date(tokenData.exp * 1000).toLocaleString(),
        now = new Date().toLocaleString();
    // console.log(now < exp);
    // console.log(props.searchParams);

    if (token && now < exp)
        return (
            <div className="w-full p-16 break-all text-center">
                <p className="mb-16">Ваш токен</p>
                <p className="mb-16">{token}</p>
                <button
                    onClick={() => {
                        console.log(props.searchParams.home);
                        navigate.push('http://' + (props.searchParams.home ?? 'localhost:3001') + `?token=${token}`);
                    }}
                    className="bg-green-400 rounded-full px-4 py-2 mb-16 font-bold"
                >
                    Перейти в приложение
                </button>
                <p className="mb-2">Истекает</p>
                <p className="">{exp}</p>
                <ClockTime />
            </div>
        );

    return (
        <>
            <div className="flex gap-2 items-center justify-between w-full p-16">
                <div className="flex flex-col w-full justify-center items-center gap-2">
                    <h1 className="text-2xl">Логин</h1>
                    <form
                        onSubmit={handleSubmit(onLogin)}
                        className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8 bg-slate-500"
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
                        <button type="submit" className="p-4 bg-gray-500 rounded-full mt-4 hover:bg-slate-800">
                            Войти
                        </button>
                    </form>
                </div>
                <div className="flex flex-col w-full justify-center items-center gap-2">
                    <h1 className="text-2xl">Регистрация</h1>
                    <form
                        onSubmit={handleSubmit2(onRegister)}
                        className="flex flex-col items-center justify-center rounded-xl text-2xl text-center w-fit p-8 bg-slate-500"
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
                        <button type="submit" className="p-4 bg-gray-500 rounded-full mt-4 hover:bg-slate-800">
                            Регистрация
                        </button>
                    </form>
                </div>
            </div>
            {token && now > exp && <p className="text-center">Токен истек</p>}
            <ClockTime />
        </>
    );
}
