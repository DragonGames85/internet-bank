'use client';

import axios from 'axios';
import { ThemeProvider as NextThemesProvider, useTheme } from 'next-themes';
import { type ThemeProviderProps } from 'next-themes/dist/types';
import { useSearchParams } from 'next/navigation';
import { useEffect } from 'react';
import { SWRConfig } from 'swr';
import { parseJwt } from './helpers/parseJwt';
import { useLocalStorage } from './hooks/useLocalStorage';
import { getToken, onMessage } from 'firebase/messaging';
import { messaging } from './accounts/components/firebase';
import { authAppUrl } from './config';

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
                    }}
                    className="text-center text-3xl text-white p-2 border-1 rounded-full border-purple-500"
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

const FireBaseProvider = ({ children }: { children: React.ReactNode }) => {
    useEffect(() => {
        const requestPermission = async () => {
            try {
                const permission = await Notification.requestPermission();
                if (permission === 'granted') {
                    const token = await getToken(messaging, {
                        vapidKey:
                            'BJ1oCTxIhn41d2s1gnagc_3TQdUImAtXYFt4YGKZ5924nZEoxlndHOMecFRLw10Pexg-1O_LwnCb2TnHWufIlPc',
                    });
                    if (token) await axios.post(authAppUrl + `/api/Device/create?token=${token}`);
                    console.log('Token:', token);
                } else {
                    console.error('Permission not granted');
                }
            } catch (error) {
                console.error('Error getting permission or token:', error);
            }
        };
        requestPermission();
        onMessage(messaging, payload => {
            console.log('Message received. ', payload);
            const { title, body } = payload.notification!;
            new Notification(title!, { body });
        });
    }, []);
    return <>{children}</>;
};

export { SwrProvider, ThemeProvider, FireBaseProvider };
