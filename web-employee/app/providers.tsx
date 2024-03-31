'use client';

import axios from 'axios';
import { SessionProvider, signOut, useSession } from 'next-auth/react';
import { ThemeProvider as NextThemesProvider } from 'next-themes';
import { type ThemeProviderProps } from 'next-themes/dist/types';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { SWRConfig } from 'swr';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './hooks/useLocalStorage';

const ThemeProvider = ({ children, ...props }: ThemeProviderProps) => {
    return <NextThemesProvider {...props}>{children}</NextThemesProvider>;
};

const AuthSessionProvider = ({ children }: { children: React.ReactNode }) => {
    return <SessionProvider>{children}</SessionProvider>;
};

const SwrProvider = ({ children }: { children: React.ReactNode }) => {
    const { data: session } = useSession() as { data: any };
    const [localToken, setToken] = useLocalStorage('token', '');
    const [_, setUser] = useLocalStorage('user', '');
    const router = useRouter();

    useEffect(() => {
        if (localToken) axios.defaults.headers.common['Authorization'] = `Bearer ${localToken}`;
        if (session && session.user) {
            const token = session.user.token;
            console.log('SESSION', { session, parsedUser: parseJwt(token) });
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            setToken(token);
            setUser(JSON.stringify(parseJwt(token)));
        }
    }, [session, localToken]);

    const user = localToken ? parseJwt(localToken) : {};

    if (user.isBanned == 'True')
        return (
            <div className="mt-52 flex-center flex-col gap-4">
                <h1 className="text-center text-3xl text-danger">ВЫ ЗАБАНЕНЫ</h1>
                <button
                    onClick={() => {
                        localStorage.removeItem('token');
                        localStorage.removeItem('user');
                        signOut();
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
                        signOut();
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

export { AuthSessionProvider, SwrProvider, ThemeProvider };
