CREATE TABLE "Bank" (
    "Id" integer NOT NULL,
	"Version" integer DEFAULT 0 NOT NULL,
    "Name" character varying
);
ALTER TABLE ONLY "Bank" ADD CONSTRAINT pk_bank_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "Bank" ADD CONSTRAINT uq_bank_name UNIQUE ("Name");
ALTER TABLE "Bank" OWNER TO postgres;
COMMENT ON COLUMN "Bank"."Id" IS 'идентификатор';
COMMENT ON COLUMN "Bank"."Version" IS 'Версия для оптимистической блокировки';


CREATE TABLE "CurrencyType" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "Name" character varying,
    "ShortName" character varying(3) NOT NULL
);
ALTER TABLE "CurrencyType" OWNER TO postgres;
ALTER TABLE ONLY "CurrencyType" ADD CONSTRAINT pk_currencytype_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "CurrencyType" ADD CONSTRAINT uq_currencytype_shortname UNIQUE ("ShortName");
COMMENT ON TABLE "CurrencyType" IS 'таблица наименований валют';
COMMENT ON COLUMN "CurrencyType"."Id" IS 'идентификатор';
COMMENT ON COLUMN "CurrencyType"."Name" IS 'наименование валюты';
COMMENT ON COLUMN "CurrencyType"."ShortName" IS 'трехбуквенное обозначение';
COMMENT ON COLUMN "CurrencyType"."Version" IS 'Версия для оптимистической блокировки';


CREATE TABLE "CourseArhive" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "CurrencyTypeId" integer NOT NULL,
    "RegDate" timestamp with time zone NOT NULL,
    "BankId" integer NOT NULL,
    "Buy" money DEFAULT 0 NOT NULL,
    "Sel" money DEFAULT 0 NOT NULL
);
ALTER TABLE "CourseArhive" OWNER TO postgres;
ALTER TABLE ONLY "CourseArhive" ADD CONSTRAINT pk_coursearhive PRIMARY KEY ("Id");
ALTER TABLE ONLY "CourseArhive" ADD CONSTRAINT uq_coursearhive_bankid_regdate_currencytypeid UNIQUE ("BankId", "RegDate", "CurrencyTypeId");
ALTER TABLE ONLY "CourseArhive" ADD CONSTRAINT fk_coursearhive_bank FOREIGN KEY ("BankId") REFERENCES "Bank"("Id") ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE ONLY "CourseArhive" ADD CONSTRAINT fk_coursearhive_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");
COMMENT ON TABLE "CourseArhive" IS 'курсы приведения валют byr-база / курс беливестбанк';
COMMENT ON COLUMN "CourseArhive"."CurrencyTypeId" IS 'валюта';
COMMENT ON COLUMN "CourseArhive"."RegDate" IS 'дата';
COMMENT ON COLUMN "CourseArhive"."BankId" IS 'Банк';
COMMENT ON COLUMN "CourseArhive"."Buy" IS 'цена покупки';
COMMENT ON COLUMN "CourseArhive"."Sel" IS 'цена продажи';
COMMENT ON COLUMN "CourseArhive"."Id" IS 'Идентификатор';
COMMENT ON COLUMN "CourseArhive"."Version" IS 'Версия для оптимистической блокировки';


CREATE TABLE "SystemUser" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "Login" character varying NOT NULL,
    "Name" character varying
);

ALTER TABLE "SystemUser" OWNER TO postgres;
ALTER TABLE ONLY "SystemUser" ADD CONSTRAINT pk_systemuser_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "SystemUser" ADD CONSTRAINT uq_systemuser_login UNIQUE ("Login");
COMMENT ON COLUMN "SystemUser"."Id" IS 'идентификатор пользователя';
COMMENT ON COLUMN "SystemUser"."Version" IS 'Версия для оптимистической блокировки';


CREATE TABLE "Wallet" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "Name" character varying,
    "StartBalance" money,
    "Balance" money,
    "SystemUserId" integer,
    "CurrencyTypeId" integer NOT NULL
);

ALTER TABLE "Wallet" OWNER TO postgres;
ALTER TABLE ONLY "Wallet" ADD CONSTRAINT pk_wallet_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "Wallet" ADD CONSTRAINT fk_wallet_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");
ALTER TABLE ONLY "Wallet" ADD CONSTRAINT fk_wallet_systemuser FOREIGN KEY ("SystemUserId") REFERENCES "SystemUser"("Id");
COMMENT ON TABLE "Wallet" IS 'пользовательские счета';
COMMENT ON COLUMN "Wallet"."Id" IS 'идентификатор';
COMMENT ON COLUMN "Wallet"."StartBalance" IS 'первоначальный баланс счета';
COMMENT ON COLUMN "Wallet"."Balance" IS 'итоговое состояние счета';
COMMENT ON COLUMN "Wallet"."SystemUserId" IS 'пользователь';
COMMENT ON COLUMN "Wallet"."CurrencyTypeId" IS 'валюта кошелька';
COMMENT ON COLUMN "Wallet"."Version" IS 'Версия для оптимистической блокировки';



CREATE TABLE "MontlyReport" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "StartBalance" money DEFAULT 0 NOT NULL,
    "EndBalance" money DEFAULT 0 NOT NULL,
    "StartDate" timestamp without time zone NOT NULL,
    "WalletId" integer NOT NULL
);

ALTER TABLE "MontlyReport" OWNER TO postgres;
ALTER TABLE ONLY "MontlyReport" ADD CONSTRAINT pk_montlyreport_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "MontlyReport" ADD CONSTRAINT pk_montlyreport_wallet FOREIGN KEY ("WalletId") REFERENCES "Wallet"("Id");
COMMENT ON TABLE "MontlyReport" IS 'пользовательский отчет за месяц';
COMMENT ON COLUMN "MontlyReport"."Id" IS 'идентификатор';
COMMENT ON COLUMN "MontlyReport"."StartBalance" IS 'баланс на начало месяца';
COMMENT ON COLUMN "MontlyReport"."EndBalance" IS 'баланс на конец месяца';
COMMENT ON COLUMN "MontlyReport"."StartDate" IS 'Период действия (год месяц)';
COMMENT ON COLUMN "MontlyReport"."WalletId" IS 'Идентификатор кошелька';
COMMENT ON COLUMN "MontlyReport"."Version" IS 'Версия для оптимистической блокировки';



CREATE TABLE "Transaction" (
    "Id" integer NOT NULL,
    "Version" integer DEFAULT 0 NOT NULL,
    "RegistrationDate" timestamp without time zone NOT NULL,
    "TransactionDate" timestamp without time zone,
    "Code" character varying,
    "Description" character varying,
    "CurrencyTypeId" integer NOT NULL,
    "QuantityByTransaction" money NOT NULL,
    "QuantityByWallet" money,
    "Commission" money,
    "FromWalletId" integer,
    "ToWalletId" integer NOT NULL,
    "MontlyReportId" integer
);
ALTER TABLE "Transaction" OWNER TO postgres;
ALTER TABLE ONLY "Transaction" ADD CONSTRAINT pk_transaction_id PRIMARY KEY ("Id");
ALTER TABLE ONLY "Transaction" ADD CONSTRAINT fk_transaction_currency FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");
ALTER TABLE ONLY "Transaction" ADD CONSTRAINT fk_transaction_montly_report FOREIGN KEY ("MontlyReportId") REFERENCES "MontlyReport"("Id") MATCH SIMPLE ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE ONLY "Transaction" ADD CONSTRAINT fk_transactionfromwallet_wallet FOREIGN KEY ("FromWalletId") REFERENCES "Wallet"("Id");
ALTER TABLE ONLY "Transaction" ADD CONSTRAINT fk_transactiontowallet_wallet FOREIGN KEY ("ToWalletId") REFERENCES "Wallet"("Id");
COMMENT ON COLUMN "Transaction"."Id" IS 'идентификатор';
COMMENT ON COLUMN "Transaction"."RegistrationDate" IS 'дата регистрации транзакции в системе';
COMMENT ON COLUMN "Transaction"."TransactionDate" IS 'дата совершения транзакции';
COMMENT ON COLUMN "Transaction"."Code" IS 'код транзакции';
COMMENT ON COLUMN "Transaction"."Description" IS 'комментарий';
COMMENT ON COLUMN "Transaction"."CurrencyTypeId" IS 'валюта транзакции';
COMMENT ON COLUMN "Transaction"."QuantityByTransaction" IS 'сумма в валюте транзакции';
COMMENT ON COLUMN "Transaction"."QuantityByWallet" IS 'сумма в валюте счета';
COMMENT ON COLUMN "Transaction"."Commission" IS 'комиссия';
COMMENT ON COLUMN "Transaction"."FromWalletId" IS 'перевод с счета';
COMMENT ON COLUMN "Transaction"."ToWalletId" IS 'перевод на счет';
COMMENT ON COLUMN "Transaction"."Version" IS 'Версия для оптимистической блокировки';