'use client';

import axios from 'axios';
import { ThemeProvider as NextThemesProvider, useTheme } from 'next-themes';
import { type ThemeProviderProps } from 'next-themes/dist/types';
import { useSearchParams } from 'next/navigation';
import router from 'next/router';
import { useEffect, useLayoutEffect, useReducer } from 'react';
import { SWRConfig } from 'swr';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './hooks/useLocalStorage';

const ThemeProvider = ({ children, ...props }: ThemeProviderProps) => {
    return <NextThemesProvider {...props}>{children}</NextThemesProvider>;
};

const SwrProvider = ({ children }: { children: React.ReactNode }) => {
    const { setTheme } = useTheme();
    const token = useSearchParams().get('token') ?? '';
    const [localToken, setToken] = useLocalStorage('token', '');
    const [_, setUser] = useLocalStorage('user', '');

    const resultToken = token ?? localToken;
    
    useEffect(() => {
        if (resultToken) {
            const userLocal = parseJwt(resultToken);
            axios.defaults.headers.common['Authorization'] = `Bearer ${resultToken}`;
            setTheme(userLocal.isLightTheme == 'True' ? 'light' : 'dark');
            localStorage.setItem('theme', userLocal.isLightTheme == 'True' ? 'light' : 'dark');
            localStorage.setItem('token', resultToken);
            setToken(resultToken);
            setUser(JSON.stringify(userLocal));
        }
    }, [token, localToken]);

    const user = resultToken ? parseJwt(resultToken) : {};

    if (user.isBanned == 'True')
        return (
            <div className="mt-52 flex-center flex-col gap-4">
                <h1 className="text-center text-3xl text-danger">ВЫ ЗАБАНЕНЫ</h1>
                <button
                    onClick={() => {
                        localStorage.removeItem('token');
                        localStorage.removeItem('user');
                        router.push('/');
                    }}
                    className="text-center text-3xl text-white p-2 border-1 rounded-full border-purple-500"
                >
                    ВЫЙТИ
                </button>
            </div>
        );

    if (user.role == 'Customer')
        return (
            <div className="mt-52 flex-center flex-col gap-4">
                <h1 className="text-center text-3xl text-danger">ВЫ НЕ СОТРУДНИК</h1>
                <button
                    onClick={() => {
                        localStorage.removeItem('token');
                        localStorage.removeItem('user');
                        router.push('/');
                    }}
                    className="text-center text-3xl text-black dark:text-white p-2 border-1 rounded-full border-purple-500"
                >
                    ВЫЙТИ
                </button>
            </div>
        );

    return (
        <SWRConfig
            value={{
                revalidateOnFocus: false,
                refreshWhenHidden: false,
                refreshWhenOffline: false,
                errorRetryCount: 3,
                // fetcher: (resource, init) => fetch(resource, init).then(res => res.json()),
            }}
        >
            {children}
        </SWRConfig>
    );
};

export { SwrProvider, ThemeProvider };
