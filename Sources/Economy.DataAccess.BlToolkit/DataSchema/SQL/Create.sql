--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.2
-- Dumped by pg_dump version 9.5.2

-- Started on 2016-08-31 08:05:39

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2146 (class 1262 OID 16639)
-- Name: economy; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE economy WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE economy OWNER TO postgres;

\connect economy

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12355)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2149 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'pl/pgsql procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 187 (class 1259 OID 16701)
-- Name: BelinvestCourseArhive; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "BelinvestCourseArhive" (
    "CurrencyTypeId" integer NOT NULL,
    "RegDate" timestamp with time zone NOT NULL,
    "Buy" double precision DEFAULT 0 NOT NULL,
    "Sel" double precision DEFAULT 0 NOT NULL
);


ALTER TABLE "BelinvestCourseArhive" OWNER TO postgres;

--
-- TOC entry 2150 (class 0 OID 0)
-- Dependencies: 187
-- Name: TABLE "BelinvestCourseArhive"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "BelinvestCourseArhive" IS 'РєСѓСЂСЃС‹ РїСЂРёРІРµРґРµРЅРёСЏ РІР°Р»СЋС‚ byr-Р±Р°Р·Р° / РєСѓСЂСЃ Р±РµР»РёРІРµСЃС‚Р±Р°РЅРє';


--
-- TOC entry 2151 (class 0 OID 0)
-- Dependencies: 187
-- Name: COLUMN "BelinvestCourseArhive"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "BelinvestCourseArhive"."CurrencyTypeId" IS 'РІР°Р»СЋС‚Р°';


--
-- TOC entry 2152 (class 0 OID 0)
-- Dependencies: 187
-- Name: COLUMN "BelinvestCourseArhive"."RegDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "BelinvestCourseArhive"."RegDate" IS 'РґР°С‚Р°';


--
-- TOC entry 2153 (class 0 OID 0)
-- Dependencies: 187
-- Name: COLUMN "BelinvestCourseArhive"."Buy"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "BelinvestCourseArhive"."Buy" IS 'С†РµРЅР° РїРѕРєСѓРїРєРё';


--
-- TOC entry 2154 (class 0 OID 0)
-- Dependencies: 187
-- Name: COLUMN "BelinvestCourseArhive"."Sel"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "BelinvestCourseArhive"."Sel" IS 'С†РµРЅР° РїСЂРѕРґР°Р¶Рё';


--
-- TOC entry 181 (class 1259 OID 16643)
-- Name: CurrencyType; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "CurrencyType" (
    "Id" integer NOT NULL,
    "Name" character varying,
    "ShortName" character varying(3) NOT NULL
);


ALTER TABLE "CurrencyType" OWNER TO postgres;

--
-- TOC entry 2155 (class 0 OID 0)
-- Dependencies: 181
-- Name: TABLE "CurrencyType"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "CurrencyType" IS 'С‚Р°Р±Р»РёС†Р° РЅР°РёРјРµРЅРѕРІР°РЅРёР№ РІР°Р»СЋС‚';


--
-- TOC entry 2156 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Id" IS 'РёРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ';


--
-- TOC entry 2157 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Name" IS 'РЅР°РёРјРµРЅРѕРІР°РЅРёРµ РІР°Р»СЋС‚С‹';


--
-- TOC entry 2158 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."ShortName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."ShortName" IS 'С‚СЂРµС…Р±СѓРєРІРµРЅРЅРѕРµ РѕР±РѕР·РЅР°С‡РµРЅРёРµ';


--
-- TOC entry 183 (class 1259 OID 16651)
-- Name: MontlyReport; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "MontlyReport" (
    "Id" uuid NOT NULL,
    "StartBalance" money,
    "EndBalance" money,
    "StartDate" timestamp without time zone NOT NULL,
    "WalletId" uuid NOT NULL
);


ALTER TABLE "MontlyReport" OWNER TO postgres;

--
-- TOC entry 2159 (class 0 OID 0)
-- Dependencies: 183
-- Name: TABLE "MontlyReport"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "MontlyReport" IS 'РїРѕР»СЊР·РѕРІР°С‚РµР»СЊСЃРєРёР№ РѕС‚С‡РµС‚ Р·Р° РјРµСЃСЏС†';


--
-- TOC entry 2160 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "MontlyReport"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."Id" IS 'РёРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ';


--
-- TOC entry 2161 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "MontlyReport"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartBalance" IS 'Р±Р°Р»Р°РЅСЃ РЅР° РЅР°С‡Р°Р»Рѕ РјРµСЃСЏС†Р°';


--
-- TOC entry 2162 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "MontlyReport"."EndBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."EndBalance" IS 'Р±Р°Р»Р°РЅСЃ РЅР° РєРѕРЅРµС† РјРµСЃСЏС†Р°';


--
-- TOC entry 2163 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "MontlyReport"."StartDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartDate" IS 'РџРµСЂРёРѕРґ РґРµР№СЃС‚РІРёСЏ (РіРѕРґ РјРµСЃСЏС†)';


--
-- TOC entry 2164 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "MontlyReport"."WalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."WalletId" IS 'РРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ РєРѕС€РµР»СЊРєР°';


--
-- TOC entry 185 (class 1259 OID 16660)
-- Name: SystemUser; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "SystemUser" (
    "Id" uuid NOT NULL,
    "Name" character varying
);


ALTER TABLE "SystemUser" OWNER TO postgres;

--
-- TOC entry 2165 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "SystemUser"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "SystemUser"."Id" IS 'РёРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏ';


--
-- TOC entry 184 (class 1259 OID 16654)
-- Name: Transaction; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Transaction" (
    "Id" uuid NOT NULL,
    "RegistrationDate" timestamp without time zone NOT NULL,
    "TransactionDate" timestamp without time zone,
    "Code" character varying,
    "Description" character varying,
    "CurrencyTypeId" integer NOT NULL,
    "QuantityByTransaction" money NOT NULL,
    "QuantityByWallet" money,
    "Commission" money,
    "FromWalletId" uuid,
    "ToWalletId" uuid NOT NULL,
    "MontlyReportId" uuid
);


ALTER TABLE "Transaction" OWNER TO postgres;

--
-- TOC entry 2166 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Id" IS 'РёРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ';


--
-- TOC entry 2167 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."RegistrationDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."RegistrationDate" IS 'РґР°С‚Р° СЂРµРіРёСЃС‚СЂР°С†РёРё С‚СЂР°РЅР·Р°РєС†РёРё РІ СЃРёСЃС‚РµРјРµ';


--
-- TOC entry 2168 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."TransactionDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."TransactionDate" IS 'РґР°С‚Р° СЃРѕРІРµСЂС€РµРЅРёСЏ С‚СЂР°РЅР·Р°РєС†РёРё';


--
-- TOC entry 2169 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Code"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Code" IS 'РєРѕРґ С‚СЂР°РЅР·Р°РєС†РёРё';


--
-- TOC entry 2170 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Description"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Description" IS 'РєРѕРјРјРµРЅС‚Р°СЂРёР№';


--
-- TOC entry 2171 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."CurrencyTypeId" IS 'РІР°Р»СЋС‚Р° С‚СЂР°РЅР·Р°РєС†РёРё';


--
-- TOC entry 2172 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."QuantityByTransaction"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByTransaction" IS 'СЃСѓРјРјР° РІ РІР°Р»СЋС‚Рµ С‚СЂР°РЅР·Р°РєС†РёРё';


--
-- TOC entry 2173 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."QuantityByWallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByWallet" IS 'СЃСѓРјРјР° РІ РІР°Р»СЋС‚Рµ СЃС‡РµС‚Р°';


--
-- TOC entry 2174 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Commission"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Commission" IS 'РєРѕРјРёСЃСЃРёСЏ';


--
-- TOC entry 2175 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."FromWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."FromWalletId" IS 'РїРµСЂРµРІРѕРґ СЃ СЃС‡РµС‚Р°';


--
-- TOC entry 2176 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."ToWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."ToWalletId" IS 'РїРµСЂРµРІРѕРґ РЅР° СЃС‡РµС‚';


--
-- TOC entry 186 (class 1259 OID 16663)
-- Name: Wallet; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Wallet" (
    "Id" uuid NOT NULL,
    "Name" character varying,
    "StartBalance" money,
    "Balance" money,
    "SystemUserId" uuid,
    "CurrencyTypeId" integer NOT NULL
);


ALTER TABLE "Wallet" OWNER TO postgres;

--
-- TOC entry 2177 (class 0 OID 0)
-- Dependencies: 186
-- Name: TABLE "Wallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "Wallet" IS 'РїРѕР»СЊР·РѕРІР°С‚РµР»СЊСЃРєРёРµ СЃС‡РµС‚Р°';


--
-- TOC entry 2178 (class 0 OID 0)
-- Dependencies: 186
-- Name: COLUMN "Wallet"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Id" IS 'РёРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ';


--
-- TOC entry 2179 (class 0 OID 0)
-- Dependencies: 186
-- Name: COLUMN "Wallet"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."StartBalance" IS 'РїРµСЂРІРѕРЅР°С‡Р°Р»СЊРЅС‹Р№ Р±Р°Р»Р°РЅСЃ СЃС‡РµС‚Р°';


--
-- TOC entry 2180 (class 0 OID 0)
-- Dependencies: 186
-- Name: COLUMN "Wallet"."Balance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Balance" IS 'РёС‚РѕРіРѕРІРѕРµ СЃРѕСЃС‚РѕСЏРЅРёРµ СЃС‡РµС‚Р°';


--
-- TOC entry 2181 (class 0 OID 0)
-- Dependencies: 186
-- Name: COLUMN "Wallet"."SystemUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."SystemUserId" IS 'РїРѕР»СЊР·РѕРІР°С‚РµР»СЊ';


--
-- TOC entry 2182 (class 0 OID 0)
-- Dependencies: 186
-- Name: COLUMN "Wallet"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."CurrencyTypeId" IS 'РІР°Р»СЋС‚Р° РєРѕС€РµР»СЊРєР°';


--
-- TOC entry 182 (class 1259 OID 16649)
-- Name: currency_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE currency_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE currency_id_seq OWNER TO postgres;

--
-- TOC entry 2183 (class 0 OID 0)
-- Dependencies: 182
-- Name: currency_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE currency_id_seq OWNED BY "CurrencyType"."Id";


--
-- TOC entry 2005 (class 2604 OID 16669)
-- Name: Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CurrencyType" ALTER COLUMN "Id" SET DEFAULT nextval('currency_id_seq'::regclass);


--
-- TOC entry 2019 (class 2606 OID 16724)
-- Name: pk_belinvest_currency_cur_data; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "BelinvestCourseArhive"
    ADD CONSTRAINT pk_belinvest_currency_cur_data PRIMARY KEY ("CurrencyTypeId", "RegDate");


--
-- TOC entry 2009 (class 2606 OID 16673)
-- Name: pk_currency_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CurrencyType"
    ADD CONSTRAINT pk_currency_id PRIMARY KEY ("Id");


--
-- TOC entry 2011 (class 2606 OID 16675)
-- Name: pk_montly_report_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montly_report_id PRIMARY KEY ("Id");


--
-- TOC entry 2013 (class 2606 OID 16677)
-- Name: pk_transaction_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT pk_transaction_id PRIMARY KEY ("Id");


--
-- TOC entry 2017 (class 2606 OID 16681)
-- Name: pk_user_account_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT pk_user_account_id PRIMARY KEY ("Id");


--
-- TOC entry 2015 (class 2606 OID 16679)
-- Name: pk_user_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SystemUser"
    ADD CONSTRAINT pk_user_id PRIMARY KEY ("Id");


--
-- TOC entry 2027 (class 2606 OID 16706)
-- Name: FK_BelinvestCourseArhive_CurrencyType; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "BelinvestCourseArhive"
    ADD CONSTRAINT "FK_BelinvestCourseArhive_CurrencyType" FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2021 (class 2606 OID 16687)
-- Name: fk_transaction_currency; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_currency FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2022 (class 2606 OID 16692)
-- Name: fk_transaction_montly_report; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_montly_report FOREIGN KEY ("MontlyReportId") REFERENCES "MontlyReport"("Id");


--
-- TOC entry 2023 (class 2606 OID 16767)
-- Name: fk_transactionfromwallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactionfromwallet_wallet FOREIGN KEY ("FromWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2024 (class 2606 OID 16772)
-- Name: fk_transactiontowallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactiontowallet_wallet FOREIGN KEY ("ToWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2025 (class 2606 OID 16777)
-- Name: fk_wallet_currencytype; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2026 (class 2606 OID 16782)
-- Name: fk_wallet_systemuser; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_systemuser FOREIGN KEY ("SystemUserId") REFERENCES "SystemUser"("Id");


--
-- TOC entry 2020 (class 2606 OID 16762)
-- Name: pk_montlyreport_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montlyreport_wallet FOREIGN KEY ("WalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2148 (class 0 OID 0)
-- Dependencies: 7
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2016-08-31 08:05:40

--
-- PostgreSQL database dump complete
--

