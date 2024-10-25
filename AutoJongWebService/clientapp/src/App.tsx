import React from "react";
import {NavBar} from "./components/NavBar/NavBar.tsx";
import {MainPage} from "./components/Pages/MainPage/mainPage.tsx";
import {ScrollContextProvider} from "./contexts/ScrollContext.tsx";
import './App.scss'

const Content: React.FC = () => {
    return <>
        <NavBar/>
        <MainPage/>
    </>
}

export const App : React.FC = () => {
    return <>
        <ScrollContextProvider>
            <Content/>
        </ScrollContextProvider>
    </>
}