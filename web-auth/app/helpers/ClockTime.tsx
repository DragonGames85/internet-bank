'use client';
import React from 'react';
import Clock from 'react-live-clock';

export default function ClockTime() {
    return (
        <div className="mt-16 text-2xl text-center">
            <p className="mb-4">ВРЕМЯ: </p>
            <Clock format={'HH:mm:ss'} noSsr locale="RUS" style={{ fontSize: '1.5em' }} ticking={true} />
        </div>
    );
}
