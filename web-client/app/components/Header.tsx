'use client';
import { PiUserRectangleBold } from 'react-icons/pi';
import { GiBanknote } from 'react-icons/gi';
import { MdCreditCard } from 'react-icons/md';
import Link from 'next/link';
import { useEffect } from 'react';
import axios from 'axios';
import ThemeSwitch from './ThemeSwitch';
import { useLocalStorage } from '../hooks/useLocalStorage';

import { useSession, signIn, signOut } from 'next-auth/react';

const HeaderComp = () => {
    // const [token, _] = useLocalStorage('token', '');

    // const { data: session } = useSession();
    let session = {};

    const links = session
        ? [
              {
                  name: 'ПРОФИЛЬ',
                  icon: <PiUserRectangleBold size={60} />,
                  to: '/',
              },
              {
                  name: 'СЧЕТА',
                  icon: <GiBanknote size={60} />,
                  to: '/accounts',
              },
              {
                  name: 'КРЕДИТЫ',
                  icon: <MdCreditCard size={60} />,
                  to: '/credits',
              },
          ]
        : [];

    const ButtonComp = ({ children, href, className }: any) => {
        return (
            <Link
                href={href}
                className={`${className} flex items-center gap-2 border-2 rounded-full px-4 lg:px-16 bg-bgColor2 dark:bg-bgColor3Dark text-3xl`}
            >
                {children}
            </Link>
        );
    };

    // useEffect(() => {
    //     if (token) {
    //         axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    //     }
    // }, []);

    return (
        <header className="flex flex-col lg:flex-row justify-between px-12 py-4 w-full">
            <div className="flex flex-col lg:flex-row gap-4 lg:gap-12">
                {links.map(item => (
                    <ButtonComp key={item.name} href={item.to} className={''}>
                        {item.icon}
                        {item.name}
                    </ButtonComp>
                ))}
            </div>
            {session && (
                <>
                    <button
                        onClick={() => signOut()}
                        className="border-2 rounded-full px-4 lg:px-16 bg-bgColor2 dark:bg-bgColor3Dark text-3xl"
                    >
                        Выйти
                    </button>
                    <div className="absolute right-10 bottom-10">
                        <ThemeSwitch />
                    </div>
                </>
            )}
        </header>
    );
};

export default HeaderComp;
