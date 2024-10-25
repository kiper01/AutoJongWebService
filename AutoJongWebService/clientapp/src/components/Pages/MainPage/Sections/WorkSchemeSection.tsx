import React, {useContext} from "react";
import "../Sections/WorkSchemeSection.scss";
import {ScrollContext} from "../../../../contexts/ScrollContext.tsx";
import {motion, useTransform} from "framer-motion";

export const WorkSchemeSection: React.FC = () => {
    const{scrollYProgress} = useContext(ScrollContext)

    return <div className="work-scheme-section">
        <div className="work-scheme-section-content-wrapper">
            <motion.div style={{
                width: '550px',
                height: '500px',
                position: 'fixed',
                top: '20%',
                right: '10%'
            }}>
                <p className="text-headline-white">
                    КАК МЫ РАБОТАЕМ
                </p>
            </motion.div>
        </div>
    </div>
}