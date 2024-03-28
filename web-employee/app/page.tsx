'use client';

import human from '@/assets/human.png';

import Image from 'next/image';

import { useSession } from 'next-auth/react';
import Login from './components/Login';

export default function Home() {
    const { data: session } = useSession();

    console.log('SESSION', session);

    return session ? (
        <div className="flex flex-col w-full items-center gap-12">
            <Image
                width={400}
                height={400}
                src={human}
                alt="profile-image"
                className="rounded-full border-[3px] object-cover pointer-events-none text-2xl select-none"
            />
            <div className="flex flex-col gap-2">
                <p className="text-xl">Вы авторизированны</p>
            </div>
        </div>
    ) : (
        <div className="w-full flex-center">
            <Login />
        </div>
    );
}
