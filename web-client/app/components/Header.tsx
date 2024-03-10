'use client';
import { PiUserRectangleBold } from 'react-icons/pi';
import { GiBanknote } from 'react-icons/gi';
import { MdCreditCard } from 'react-icons/md';
import Link from 'next/link';
import { useEffect } from 'react';
import axios from 'axios';
export const HeaderComp = () => {
    const links = !localStorage.getItem('token')
        ? []
        : [
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
          ];

    const Button = ({ children, href, className }: any) => {
        return (
            <Link
                href={href}
                className={`${className} flex items-center gap-2 border-2 rounded-full px-4 lg:px-16 bg-gray-900 hover:bg-gray-700 text-3xl`}
            >
                {children}
            </Link>
        );
    };

    useEffect(() => {
        if (localStorage.getItem('token')) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        }
    }, []);

    return (
        <header className="flex flex-col lg:flex-row justify-between px-12 py-4 w-full">
            <div className="flex flex-col lg:flex-row gap-4 lg:gap-12">
                {links.map(item => (
                    <Button key={item.name} href={item.to} className={''}>
                        {item.icon}
                        <p>{item.name}</p>
                    </Button>
                ))}
            </div>
        </header>
    );
};
