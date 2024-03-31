'use client';
import Link from 'next/link';
import { GiBanknote } from 'react-icons/gi';
import { MdCreditCard } from 'react-icons/md';
import { PiUserRectangleBold } from 'react-icons/pi';
import { useLocalStorage } from '../hooks/useLocalStorage';
import ThemeSwitch from './ThemeSwitch';

import { signOut, useSession } from 'next-auth/react';

const HeaderComp = () => {
    const [token, _] = useLocalStorage('token', '');

    const { data: session } = useSession();

    const links =
        session || token
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
            {(session || token) && (
                <>
                    <button
                        onClick={() => {
                            localStorage.removeItem('token');
                            localStorage.removeItem('user');
                            signOut();
                        }}
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
