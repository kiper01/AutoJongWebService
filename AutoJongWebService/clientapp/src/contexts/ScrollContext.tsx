import React, {createContext, PropsWithChildren} from 'react';
import {MotionValue, useScroll} from "framer-motion";

interface IProps {
    scrollX: MotionValue<number>;
    scrollY: MotionValue<number>;
    scrollXProgress: MotionValue<number>;
    scrollYProgress: MotionValue<number>;
}

export const ScrollContext = createContext<IProps>({
    scrollX: {} as MotionValue<number>,
    scrollY: {} as MotionValue<number>,
    scrollXProgress: {} as MotionValue<number>,
    scrollYProgress: {} as MotionValue<number>,
})

export const ScrollContextProvider: React.FC<{ children: React.ReactNode }> = ({ children }: PropsWithChildren) => {
    const {scrollX, scrollY, scrollXProgress, scrollYProgress} = useScroll();
    
    return <ScrollContext.Provider value={{
        scrollX,
        scrollY,
        scrollXProgress,
        scrollYProgress,
    }}>
        {children}
    </ScrollContext.Provider>
}