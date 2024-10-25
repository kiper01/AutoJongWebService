import React, {useContext} from "react";
import "../Sections/WelcomeSection.scss";
import {ScrollContext} from "../../../../contexts/ScrollContext.tsx";
import {motion, useTransform} from "framer-motion";
import bigCarImg from "../../../../images/bigcar.webp";

export const WelcomeSection: React.FC = () => {
    const {scrollYProgress} = useContext(ScrollContext)

    const bigCarScale = useTransform(scrollYProgress, [0, 0.24], [0.85, 0.4]);
    const bigCarLocation = useTransform(scrollYProgress, [0, 0.25, 0.5], [0, 100, -1000])

    return <div className="welcome-section">
        <div className="welcome-section-content-wrapper">
            <div className="big-car-wrapper">
                <motion.img
                    className="big-car"
                    style={{
                        y: bigCarLocation,
                        scale: bigCarScale
                    }}
                    src={bigCarImg}
                />
            </div>

            <div className="text-wrapper">
                <div className="text-container">
                    <p className="text-sub-headline">
                        Привет! Мы - AutoJong, и мы
                    </p>
                    <p className="text-headline-red">
                        ВОЗИМ ТАЧКИ ИЗ ЯПОНИИ, КОРЕИ И КИТАЯ
                    </p>
                </div>

                <div className="text-container">
                    <p className="text-sub-headline">
                        Привет! Мы - AutoJong, и мы
                    </p>
                    <p className="text-headline-red">
                        СУПЕР-ПУПЕР ТЕКСТ
                    </p>
                </div>
            </div>
        </div>
    </div>
}