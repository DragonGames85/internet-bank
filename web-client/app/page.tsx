'use client';
import { useEffect, useState } from 'react';
import { DaDataFio, DaDataSuggestion, FioSuggestions } from 'react-dadata';

import MALE from '@/assets/MALE.png';
import FEMALE from '@/assets/FEMALE.png';
import SAME from '@/assets/SAME.png';

import Login from './components/Login';
import 'react-dadata/dist/react-dadata.css';
import Image from 'next/image';

export default function Home() {
    const [dadata, setDadata] = useState<DaDataSuggestion<DaDataFio> | undefined>();
    const [name, setName] = useState<string>();
    const [image, setImage] = useState(MALE);

    console.log(name);

    useEffect(() => {
        document.documentElement.style.setProperty('--background-end-rgb', '0 80 0');
    }, []);

    useEffect(() => {
        setName(dadata?.data.name ?? '');
        switch (dadata?.data.gender) {
            case 'FEMALE':
                setImage(FEMALE);
                break;
            case 'MALE':
                setImage(MALE);
                break;
            case 'UNKNOWN':
                setImage(SAME);
                break;
            default:
                setImage(SAME);
                break;
        }
    }, [dadata]);

    let token = localStorage.getItem('token');

    return token ? (
        <div className="flex flex-col w-full items-center text-black gap-12">
            <Image
                width={400}
                height={400}
                src={image}
                alt="profile-image"
                className="rounded-full border-[3px] object-cover pointer-events-none text-2xl"
            />
            <div className="flex flex-col gap-2">
                <p className="text-white text-xl">Токен: {token}</p>
                <div className="flex items-center gap-2">
                    {/* <FioSuggestions
                        token="72bcb1ac7f6c6951b537dd41c247700242b412aa"
                        value={dadata}
                        onChange={setDadata}
                        inputProps={{
                            value: name,
                            onChange(e) {
                                setName(e.currentTarget.value);
                            },
                            style: {
                                width: '400px',
                                height: '50px',
                                fontSize: '1.5rem',
                            },
                        }}
                    /> */}
                </div>
            </div>
        </div>
    ) : (
        <Login />
    );
}
