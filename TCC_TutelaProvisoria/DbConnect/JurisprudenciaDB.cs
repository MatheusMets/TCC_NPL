using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_TutelaProvisoria.Entities;

namespace TCC_TutelaProvisoria.DbConnect
{
    public abstract class JurisprudenciaDB
    {

        public static void SalvaPesquisaJurisprudenciaNoBanco(PesquisaJurisprudencia PJ)
        {
            int IdentificadorJurisprudencia = -1;
            int IdentificadorPesquisa = BaseDB.RunScriptAndReturnIntValue(@"    DECLARE @IdPesquisa INT

                                                                                INSERT INTO [dbo].[Pesquisa]
                                                                                            ([Pesquisa])
                                                                                        VALUES
                                                                                            ('" + PJ.Pesquisa + "')" + 

                                                                                "SET @IdPesquisa = SCOPE_IDENTITY();" + 
                                                                                "SELECT @IdPesquisa ");

            foreach (Jurisprudencia jurisprudencia in PJ.Jurisprudencias)
            {
                IdentificadorJurisprudencia = SalvaJurisprudenciaNoBanco(jurisprudencia);

                BaseDB.RunSQLScript(@"  INSERT INTO[dbo].[PesquisaJurisprudencia]
                                                   ([IdentificadorPesquisa]
                                                   ,[IdentificadorJurisprudencia])
                                             VALUES
                                                   (" + IdentificadorPesquisa + " , " +
                                                    IdentificadorJurisprudencia + ")"       
                                   );
            }
        }

        public static int SalvaJurisprudenciaNoBanco(Jurisprudencia J)
        {
           return BaseDB.RunScriptAndReturnIntValue(@"  DECLARE @IdJurisprudencia INT

                                                        INSERT INTO [dbo].[Jurisprudencia]
                                                                    ([Processo]
                                                                    ,[Relator]
                                                                    ,[OrgaoJulgador]
                                                                    ,[Sumula]
                                                                    ,[DataJulgamento]
                                                                    ,[DataPublicacao]
                                                                    ,[Ementa]
                                                                    ,[InteiroTeor])
                                                                VALUES
                                                                    ('" + J.Processo + "' , '" + 
                                                                            J.Relator + "' , '" + 
                                                                            J.OrgaoJulgador + "' , '" +
                                                                            J.Sumula + "' , '" + 
                                                                            J.DataJulgamento + "' , '" + 
                                                                            J.DataPublicacao + "' , '" +
                                                                            J.Ementa + "' , '" + 
                                                                            J.InteiroTeor + "');" + 
                                                                            " SET @IdJurisprudencia = SCOPE_IDENTITY() " + 
                                                                            " SELECT @IdJurisprudencia ");
        }

    }
}
