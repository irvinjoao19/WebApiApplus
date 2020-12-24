using Entidades;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class NegocioDao
    {
        private static string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public static Usuario GetOne(Query q)
        {
            try
            {
                Usuario u = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Movil_GetUsuario", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = q.login;
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                u = new Usuario();
                                if (q.pass == dr.GetString(7))
                                {
                                    u.usuarioId = dr.GetInt32(0);
                                    u.documento = dr.GetString(1);
                                    u.apellidos = dr.GetString(2);
                                    u.nombres = dr.GetString(3);
                                    u.email = dr.GetString(4);
                                    u.perfilId = dr.GetInt32(5);
                                    u.estado = dr.GetInt32(8);
                                    u.mensaje = "Go";
                                }
                                else
                                {
                                    u.mensaje = "Pass";
                                }
                            }
                        }
                    }
                    cn.Close();
                }
                return u;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static string EncriptarClave(string cExpresion, bool bEncriptarCadena)
        {
            string cResult = "";
            string cPatron = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwwyz";
            string cEncrip = "^çºªæÆöûÿø£Ø×ƒ¬½¼¡«»ÄÅÉêèï7485912360^çºªæÆöûÿø£Ø×ƒ¬½¼¡«»ÄÅÉêèï";


            if (bEncriptarCadena == true)
            {
                cResult = CHRTRAN(cExpresion, cPatron, cEncrip);
            }
            else
            {
                cResult = CHRTRAN(cExpresion, cEncrip, cPatron);
            }

            return cResult;

        }

        public static string CHRTRAN(string cExpresion, string cPatronBase, string cPatronReemplazo)
        {
            string cResult = "";

            int rgChar;
            int nPosReplace;

            for (rgChar = 1; rgChar <= Strings.Len(cExpresion); rgChar++)
            {
                nPosReplace = Strings.InStr(1, cPatronBase, Strings.Mid(cExpresion, rgChar, 1));

                if (nPosReplace == 0)
                {
                    nPosReplace = rgChar;
                    cResult = cResult + Strings.Mid(cExpresion, nPosReplace, 1);
                }
                else
                {
                    if (nPosReplace > cPatronReemplazo.Length)
                    {
                        nPosReplace = rgChar;
                        cResult = cResult + Strings.Mid(cExpresion, nPosReplace, 1);
                    }
                    else
                    {
                        cResult = cResult + Strings.Mid(cPatronReemplazo, nPosReplace, 1);
                    }
                }
            }
            return cResult;
        }

        public static Sync GetSincronizar(int id, string version)
        {
            try
            {
                Sync s = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmdVersion = con.CreateCommand();
                    cmdVersion.CommandTimeout = 0;
                    cmdVersion.CommandType = CommandType.StoredProcedure;
                    cmdVersion.CommandText = "Movil_GetVersion";
                    cmdVersion.Parameters.Add("@version", SqlDbType.VarChar).Value = version;

                    SqlDataReader drVersion = cmdVersion.ExecuteReader();
                    if (drVersion.HasRows)
                    {
                        s = new Sync();

                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Movil_GetGrupo";
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            List<Grupo> g = new List<Grupo>();
                            while (dr.Read())
                            {
                                g.Add(new Grupo()
                                {
                                    grupoId = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    detalleId = dr.GetInt32(2),
                                    codigo = dr.GetString(3),
                                    descripcion = dr.GetString(4),
                                    vencimiento = dr.GetString(5),
                                    valor = dr.GetString(6),
                                    estado = dr.GetInt32(7)
                                });
                            }
                            s.grupos = g;
                        }

                        SqlCommand cmdT = con.CreateCommand();
                        cmdT.CommandTimeout = 0;
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.CommandText = "Movil_GetPuestoTierra";
                        SqlDataReader drT = cmdT.ExecuteReader();
                        if (drT.HasRows)
                        {
                            List<PuestoTierra> t = new List<PuestoTierra>();
                            while (drT.Read())
                            {
                                t.Add(new PuestoTierra()
                                {
                                    proyectadoId = drT.GetInt32(0),
                                    grupo = drT.GetString(1),
                                    resistividad = drT.GetString(2),
                                    pozo = drT.GetString(3),
                                    dosis = drT.GetString(4),
                                    tratado = drT.GetString(5),
                                    estado = drT.GetInt32(6)
                                });
                            }
                            s.puestos = t;
                        }

                        SqlCommand cmdC = con.CreateCommand();
                        cmdC.CommandTimeout = 0;
                        cmdC.CommandType = CommandType.StoredProcedure;
                        cmdC.CommandText = "Movil_GetFormato";
                        SqlDataReader drC = cmdC.ExecuteReader();
                        if (drC.HasRows)
                        {
                            List<Formato> f = new List<Formato>();
                            while (drC.Read())
                            {
                                f.Add(new Formato()
                                {
                                    formatoId = drC.GetInt32(0),
                                    nombre = drC.GetString(1),
                                    estado = drC.GetInt32(2)
                                });
                            }
                            s.formatos = f;
                        }

                        SqlCommand cmdS = con.CreateCommand();
                        cmdS.CommandTimeout = 0;
                        cmdS.CommandType = CommandType.StoredProcedure;
                        cmdS.CommandText = "DSIGE_Proy_M_tbl_sed_ListaSolo_Sed";
                        SqlDataReader drS = cmdS.ExecuteReader();
                        if (drS.HasRows)
                        {
                            List<Sed> d = new List<Sed>();
                            while (drS.Read())
                            {
                                d.Add(new Sed()
                                {
                                    codigo = drS.GetString(0),
                                    alimentador = drS.GetString(1)
                                });
                            }
                            s.seds = d;
                        }

                        SqlCommand cmdSU = con.CreateCommand();
                        cmdSU.CommandTimeout = 0;
                        cmdSU.CommandType = CommandType.StoredProcedure;
                        cmdSU.CommandText = "Movil_GetSupervisor";
                        SqlDataReader drSU = cmdSU.ExecuteReader();
                        if (drSU.HasRows)
                        {
                            List<Supervisor> d = new List<Supervisor>();
                            while (drSU.Read())
                            {
                                d.Add(new Supervisor()
                                {
                                    supervisorId = drSU.GetInt32(0),
                                    nombre = drSU.GetString(1)
                                });
                            }
                            s.supervisores = d;
                        }

                        SqlCommand cmdE = con.CreateCommand();
                        cmdE.CommandTimeout = 0;
                        cmdE.CommandType = CommandType.StoredProcedure;
                        cmdE.CommandText = "DSIGE_Proy_M_tbl_Estados_Lista";
                        cmdE.Parameters.Add("@Usuario", SqlDbType.Int).Value = id;
                        SqlDataReader drE = cmdE.ExecuteReader();
                        if (drE.HasRows)
                        {
                            List<Estado> d = new List<Estado>();
                            while (drE.Read())
                            {
                                d.Add(new Estado()
                                {
                                    estadoId = drE.GetInt32(0),
                                    abreviatura = drE.GetString(1),
                                    descripcion = drE.GetString(2),
                                    tipoProceso = drE.GetString(3),
                                    descripcionTP = drE.GetString(4),
                                    moduloId = drE.GetInt32(5),
                                    orden = drE.GetInt32(6),
                                    backColor = drE.GetString(7),
                                    foreColor = drE.GetString(8),
                                    activo = drE.GetInt32(9)
                                });
                            }
                            s.estadoTrabajo = d;
                        }

                        SqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandTimeout = 0;
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandText = "Dsige_Proy_M_Estado_Poste";
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            List<Estado> d = new List<Estado>();
                            while (dr2.Read())
                            {
                                d.Add(new Estado()
                                {
                                    estadoId = dr2.GetInt32(0),
                                    abreviatura = dr2.GetString(1),
                                    descripcion = dr2.GetString(2),
                                    tipoProceso = dr2.GetString(3),
                                    descripcionTP = dr2.GetString(4),
                                    moduloId = dr2.GetInt32(5),
                                    orden = dr2.GetInt32(6),
                                    backColor = dr2.GetString(7),
                                    foreColor = dr2.GetString(8),
                                    activo = dr2.GetInt32(9)
                                });
                            }
                            s.estadoPoste = d;
                        }

                        SqlCommand cmdCA = con.CreateCommand();
                        cmdCA.CommandTimeout = 0;
                        cmdCA.CommandType = CommandType.StoredProcedure;
                        cmdCA.CommandText = "DSIGE_Proy_M_tbl_CadistaCantidadPendiente";
                        SqlDataReader drCA = cmdCA.ExecuteReader();
                        if (drCA.HasRows)
                        {
                            List<Cadista> d = new List<Cadista>();
                            while (drCA.Read())
                            {
                                d.Add(new Cadista()
                                {
                                    cadistaId = drCA.GetInt32(0),
                                    nombre = drCA.GetString(1),
                                    pendientes = drCA.GetInt32(2)
                                });
                            }
                            s.cadistas = d;
                        }

                        SqlCommand cmdO = con.CreateCommand();
                        cmdO.CommandTimeout = 0;
                        cmdO.CommandType = CommandType.StoredProcedure;
                        cmdO.CommandText = "DSIGE_Proy_M_tbl_OT_Lista";
                        cmdO.Parameters.Add("@proyectista", SqlDbType.VarChar).Value = id;
                        cmdO.Parameters.Add("@Estado", SqlDbType.VarChar).Value = 1;
                        SqlDataReader drO = cmdO.ExecuteReader();
                        if (drO.HasRows)
                        {
                            List<Ot> o = new List<Ot>();
                            while (drO.Read())
                            {
                                Ot t = new Ot();
                                t.otId = drO.GetInt32(0);
                                t.numeroOt = drO.GetString(1);
                                t.nroOt = drO.GetString(2);
                                t.descripcion = drO.GetString(3);
                                t.nombre = drO.GetString(4);
                                t.direccion = drO.GetString(5);
                                t.distrito = drO.GetString(6);
                                t.fechaLegal = drO.GetDateTime(7).ToString("dd/MM/yyyy H:mm");
                                t.fechaAsignacion = drO.GetDateTime(8).ToString("dd/MM/yyyy H:mm");
                                t.proyectistaId = drO.GetInt32(9);
                                t.fechaRecepcion = drO.GetDateTime(10).ToString("dd/MM/yyyy H:mm");
                                t.estado = drO.GetString(11);
                                t.diasVencimiento = drO.GetInt32(12);
                                t.estadoId = drO.GetInt32(13);
                                t.active = 0;
                                t.comentario = drO.GetString(14);
                                t.motivoId = drO.GetInt32(15);
                                t.coordinadorId = drO.GetInt32(16);
                                t.nombreCoordinador = drO.GetString(17);
                                t.supervisorId = drO.GetInt32(18);
                                t.nombreSupervisor = drO.GetString(19);

                                SqlCommand cmdOC = con.CreateCommand();
                                cmdOC.CommandTimeout = 0;
                                cmdOC.CommandType = CommandType.StoredProcedure;
                                cmdOC.CommandText = "DSIGE_Proy_M_Formato_x_OT";
                                cmdOC.Parameters.Add("@id_OT", SqlDbType.Int).Value = t.otId;
                                cmdOC.Parameters.Add("@Formato", SqlDbType.Int).Value = 1;

                                SqlDataReader drOC = cmdOC.ExecuteReader();
                                if (drOC.HasRows)
                                {
                                    List<OtCabecera> c = new List<OtCabecera>();
                                    while (drOC.Read())
                                    {
                                        c.Add(new OtCabecera()
                                        {
                                            formatoId = drOC.GetInt32(0),
                                            tipoFormatoId = drOC.GetInt32(1),
                                            nombreTipoFormato = drOC.GetString(2),
                                            seccion = drOC.GetString(3),
                                            otId = drOC.GetInt32(4),
                                            nroOt = drOC.GetString(5),
                                            nombrecliente = drOC.GetString(6),
                                            alimentador = drOC.GetString(7),
                                            modulo = drOC.GetString(8),
                                            sed = drOC.GetString(9),
                                            ubicacion = drOC.GetString(10),
                                            distrito = drOC.GetString(11),
                                            ubicacionSed = drOC.GetString(12),
                                            coordenadaX = drOC.GetString(13),
                                            coordenadaY = drOC.GetString(14),
                                            fechaRegistro = drOC.GetDateTime(15).ToString("dd/MM/yyyy H:mm"),
                                            estadoId = drOC.GetInt32(16),
                                            nombreEstado = drOC.GetString(17),
                                            convencional = drOC.GetString(18),
                                            compacta = drOC.GetString(19),
                                            aerea = drOC.GetString(20),
                                            pmi = drOC.GetString(21),
                                            aNivel = drOC.GetString(22),
                                            pedestal = drOC.GetString(23),
                                            monoposte = drOC.GetString(24),
                                            reCloser = drOC.GetString(25),
                                            subTerranea = drOC.GetString(26),
                                            boveda = drOC.GetString(27),
                                            biposte = drOC.GetString(28),
                                            sbc = drOC.GetString(29),
                                            soporte = drOC.GetString(30),
                                            setProtocolo = drOC.GetString(31),
                                            cuadrilla = drOC.GetString(32),
                                            lamina = drOC.GetString(33),
                                            letra = drOC.GetString(34),
                                            seccionId = 1,
                                            dibujar = ""
                                        });
                                    }
                                    t.otCabecera = c;
                                }
                                o.Add(t);
                            }
                            s.ots = o;
                        }

                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandTimeout = 0;
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.CommandText = "DSIGE_Proy_M_tbl_Postes_Lista";
                        cmd3.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = id;
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        if (dr3.HasRows)
                        {
                            List<InspeccionPoste> d = new List<InspeccionPoste>();
                            while (dr3.Read())
                            {
                                d.Add(new InspeccionPoste()
                                {
                                    inspeccionCampoId = dr3.GetInt32(0),
                                    inspeccionId = dr3.GetInt32(1),
                                    itemGeneral = dr3.GetInt32(2),
                                    codPoste = dr3.GetString(3),
                                    distritoCod = dr3.GetString(4),
                                    direccion = dr3.GetString(5),
                                    nlote = dr3.GetString(6),
                                    tipoCablePre = dr3.GetString(7),
                                    cantCable = dr3.GetInt32(8),
                                    estadoId = dr3.GetInt32(9),
                                    nombreEstado = dr3.GetString(10),
                                    latitud = dr3.GetString(11),
                                    longitud = dr3.GetString(12),
                                    utmX = "",
                                    utmY = "",
                                    nroPoste = "",
                                    id_MaterialSoporte = 0,
                                    id_TipoPoste = 0,
                                    id_CargaTrabajo = 0,
                                    id_TipoArmado = 0,
                                    alturaPoste = 0,
                                    alturaLibrePoste = 0,
                                    id_estadoMensula = 0,
                                    id_NivelCriticidad = 0,
                                    comentariosGenerales = "",
                                    vanoMT_AngIzq = 0,
                                    vanoMT_AngDer = 0,
                                    vanoMT_AngAde = 0,
                                    vanoMT_AngAtra = 0,
                                    vanoMT_VanIzq = 0,
                                    vanoMT_VanDer = 0,
                                    vanoMT_VanAde = 0,
                                    vanoMT_VanAtra = 0,
                                    comentariosVanoMt = "",
                                    vanoBT_AngIzq = 0,
                                    vanoBT_AngDer = 0,
                                    vanoBT_AngAde = 0,
                                    vanoBT_AngAtra = 0,
                                    vanoBT_VanIzq = 0,
                                    vanoBT_VanDer = 0,
                                    vanoBT_VanAde = 0,
                                    vanoBT_VanAtra = 0,
                                    AngRetenidaBt = 0,
                                    comentariosVanoBt = "",
                                    id_reteBT_tipoR1 = 0,
                                    reteBT_AlturaR1 = 0,
                                    reteBT_DirR1 = "",
                                    id_reteBT_Estado1 = 0,
                                    id_reteBT_tipoR2 = 0,
                                    reteBT_AlturaR2 = 0,
                                    reteBT_DirR2 = "",
                                    id_reteBT_Estado2 = 0,
                                    comentarios_ReteBT = "",
                                    infoAdd_RolloReserva = "",
                                    infoAdd_Fotos = "",
                                    id_infoAdd_Consecion = 0,
                                    infoAdd_Niveltension = "",
                                    infoCnew_CableNuevo = "",
                                    infoCnew_vanoIzq = 0,
                                    infoCnew_vanoDer = 0,
                                    infoCnew_vanoAde = 0,
                                    infoCnew_vanoAtra = 0,
                                    infoCnew_alturaInstala = 0,
                                    obsC_Cumplimiento = 0,
                                    obsC_PosteInclinado = 0,
                                    obsC_PosteSubida = 0,
                                    obsC_PosteSaturado = 0,
                                    comentarios_obsC = "",
                                    resFac_Factible = 0,
                                    id_resFac_ObsPrincipal = 0,
                                    Obs_generales = "",
                                    usuarioId = 0,
                                });
                            }
                            s.otsPostes = d;
                        }
                    }
                    con.Close();
                }
                return s;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static Mensaje SaveTrabajo(OtCabecera r)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_SaveCabecera";
                    cmd.Parameters.Add("@formatoId", SqlDbType.Int).Value = r.identity;
                    cmd.Parameters.Add("@tipoFormatoId", SqlDbType.Int).Value = r.tipoFormatoId;
                    cmd.Parameters.Add("@seccionId", SqlDbType.Int).Value = r.seccionId;
                    cmd.Parameters.Add("@otId", SqlDbType.Int).Value = r.otId;
                    cmd.Parameters.Add("@nombreCliente", SqlDbType.VarChar).Value = r.nombreTipoFormato;
                    cmd.Parameters.Add("@alimentador", SqlDbType.VarChar).Value = r.alimentador;
                    cmd.Parameters.Add("@modulo", SqlDbType.VarChar).Value = r.modulo;
                    cmd.Parameters.Add("@sed", SqlDbType.VarChar).Value = r.sed;
                    cmd.Parameters.Add("@ubicacion", SqlDbType.VarChar).Value = r.ubicacion;
                    cmd.Parameters.Add("@distritoId", SqlDbType.VarChar).Value = r.distrito;
                    cmd.Parameters.Add("@ubicacionSed", SqlDbType.VarChar).Value = r.ubicacion;
                    cmd.Parameters.Add("@coordX", SqlDbType.VarChar).Value = r.coordenadaX;
                    cmd.Parameters.Add("@coordY", SqlDbType.VarChar).Value = r.coordenadaY;

                    cmd.Parameters.Add("@convencional", SqlDbType.VarChar).Value = r.convencional;
                    cmd.Parameters.Add("@compacta", SqlDbType.VarChar).Value = r.compacta;
                    cmd.Parameters.Add("@aerea", SqlDbType.VarChar).Value = r.aerea;
                    cmd.Parameters.Add("@pmi", SqlDbType.VarChar).Value = r.pmi;
                    cmd.Parameters.Add("@anivel", SqlDbType.VarChar).Value = r.aNivel;
                    cmd.Parameters.Add("@pedestal", SqlDbType.VarChar).Value = r.pedestal;
                    cmd.Parameters.Add("@monoposte", SqlDbType.VarChar).Value = r.monoposte;
                    cmd.Parameters.Add("@recloser", SqlDbType.VarChar).Value = r.reCloser;
                    cmd.Parameters.Add("@subterranea", SqlDbType.VarChar).Value = r.subTerranea;
                    cmd.Parameters.Add("@boveda", SqlDbType.VarChar).Value = r.boveda;
                    cmd.Parameters.Add("@biposte", SqlDbType.VarChar).Value = r.biposte;
                    cmd.Parameters.Add("@sbc", SqlDbType.VarChar).Value = r.sbc;
                    cmd.Parameters.Add("@soporte", SqlDbType.VarChar).Value = r.soporte;
                    cmd.Parameters.Add("@setProtocolo", SqlDbType.VarChar).Value = r.setProtocolo;
                    cmd.Parameters.Add("@cuadriculla", SqlDbType.VarChar).Value = r.cuadrilla;
                    cmd.Parameters.Add("@lamina", SqlDbType.VarChar).Value = r.lamina;
                    cmd.Parameters.Add("@letra", SqlDbType.VarChar).Value = r.letra;

                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = r.fechaRegistro;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                    cmd.Parameters.Add("@cadistaId", SqlDbType.Int).Value = r.cadistaId;
                    cmd.Parameters.Add("@dibujar", SqlDbType.VarChar).Value = r.dibujar;
                    cmd.Parameters.Add("@supervisorId", SqlDbType.Int).Value = r.supervisorId;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();

                        while (dr.Read())
                        {
                            m.mensaje = "Mensaje Enviado";
                            m.codigoBase = r.formatoId;
                            m.codigoRetorno = dr.GetInt32(0);

                            foreach (var b in r.details)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_SaveDetalleMTBT";
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@codigoSoporte", SqlDbType.VarChar).Value = b.codigoSoporte;
                                cmdB.Parameters.Add("@alim", SqlDbType.VarChar).Value = b.alim;
                                cmdB.Parameters.Add("@armado", SqlDbType.VarChar).Value = b.armado;
                                cmdB.Parameters.Add("@tipoMaterialId", SqlDbType.VarChar).Value = b.tipoMaterialId;
                                cmdB.Parameters.Add("@tamanio", SqlDbType.VarChar).Value = b.tamanio;
                                cmdB.Parameters.Add("@funcionId", SqlDbType.VarChar).Value = b.funcionId;


                                cmdB.Parameters.Add("@redSDS", SqlDbType.VarChar).Value = b.redSDS;
                                cmdB.Parameters.Add("@redAP", SqlDbType.VarChar).Value = b.redAP;
                                cmdB.Parameters.Add("@redAmbas", SqlDbType.VarChar).Value = b.redAmbas;
                                cmdB.Parameters.Add("@seccNumeroId", SqlDbType.Int).Value = b.cNumeroId;
                                cmdB.Parameters.Add("@seccCod", SqlDbType.VarChar).Value = b.seccCod;
                                cmdB.Parameters.Add("@seccCap", SqlDbType.VarChar).Value = b.seccCap;
                                cmdB.Parameters.Add("@seccFus", SqlDbType.VarChar).Value = b.seccFus;


                                cmdB.Parameters.Add("@tipoConductorId", SqlDbType.VarChar).Value = b.tipoConductorId;
                                cmdB.Parameters.Add("@lvano  ", SqlDbType.VarChar).Value = b.lvano;
                                cmdB.Parameters.Add("@conduSecc  ", SqlDbType.VarChar).Value = b.conduSecc;
                                cmdB.Parameters.Add("@conduFases ", SqlDbType.VarChar).Value = b.conduFases;
                                cmdB.Parameters.Add("@tipoAisladorId ", SqlDbType.VarChar).Value = b.tipoAisladorId;
                                cmdB.Parameters.Add("@aislaMaterial ", SqlDbType.VarChar).Value = b.aislaMaterial;
                                cmdB.Parameters.Add("@aislaCantidad ", SqlDbType.VarChar).Value = b.aislaCantidad;
                                cmdB.Parameters.Add("@vientoViolin  ", SqlDbType.VarChar).Value = b.vientoViolin;
                                cmdB.Parameters.Add("@vientoSimple  ", SqlDbType.VarChar).Value = b.vientoSimple;
                                cmdB.Parameters.Add("@vientoCantidad ", SqlDbType.VarChar).Value = b.vientoCantidad;
                                cmdB.Parameters.Add("@pastoral", SqlDbType.VarChar).Value = b.pastoral;
                                cmdB.Parameters.Add("@observaciones", SqlDbType.VarChar).Value = b.observaciones;
                                cmdB.Parameters.Add("@codigoVia", SqlDbType.VarChar).Value = b.codigoVia;
                                cmdB.Parameters.Add("@llave", SqlDbType.VarChar).Value = b.llave;
                                cmdB.Parameters.Add("@sistemas", SqlDbType.VarChar).Value = b.sistemas;
                                cmdB.Parameters.Add("@cajaDeriva", SqlDbType.VarChar).Value = b.cajaDeriva;
                                cmdB.Parameters.Add("@retenidaV", SqlDbType.VarChar).Value = b.retenidaV;
                                cmdB.Parameters.Add("@retenidaS", SqlDbType.VarChar).Value = b.retenidaS;
                                cmdB.Parameters.Add("@pastotalC", SqlDbType.VarChar).Value = b.pastotalC;
                                cmdB.Parameters.Add("@pastotalGF", SqlDbType.VarChar).Value = b.pastotalGF;
                                cmdB.Parameters.Add("@equipoTipo", SqlDbType.VarChar).Value = b.equipoTipo;
                                cmdB.Parameters.Add("@equipoModelo", SqlDbType.VarChar).Value = b.equipoModelo;
                                cmdB.Parameters.Add("@lampara", SqlDbType.VarChar).Value = b.lampara;
                                cmdB.Parameters.Add("@direccion", SqlDbType.VarChar).Value = b.direccion;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.equipos)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_Save_Equipo";
                                cmdB.Parameters.Add("@equipoId", SqlDbType.Int).Value = b.equipoId;
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@tipoEquipo", SqlDbType.Int).Value = b.tipoEquipo;

                                cmdB.Parameters.Add("@nroKardex", SqlDbType.VarChar).Value = b.nroKardex;
                                cmdB.Parameters.Add("@nroFabrica", SqlDbType.VarChar).Value = b.nroFabrica;
                                cmdB.Parameters.Add("@sedUbicacion", SqlDbType.VarChar).Value = b.sedUbicacion;
                                cmdB.Parameters.Add("@celdaUbicacion", SqlDbType.VarChar).Value = b.celdaUbicacion;
                                cmdB.Parameters.Add("@potenciaKVA", SqlDbType.VarChar).Value = b.potenciaKVA;
                                cmdB.Parameters.Add("@anio", SqlDbType.VarChar).Value = b.anio;
                                cmdB.Parameters.Add("@marca", SqlDbType.VarChar).Value = b.marca;
                                cmdB.Parameters.Add("@tipo", SqlDbType.VarChar).Value = b.tipo;
                                cmdB.Parameters.Add("@Destino", SqlDbType.VarChar).Value = b.destino;
                                cmdB.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = b.observacion;
                                cmdB.Parameters.Add("@funcionCelda", SqlDbType.VarChar).Value = b.funcionCelda;
                                cmdB.Parameters.Add("@enlace", SqlDbType.VarChar).Value = b.enlace;
                                cmdB.Parameters.Add("@equipo", SqlDbType.VarChar).Value = b.equipo;
                                cmdB.Parameters.Add("@nroPrc", SqlDbType.VarChar).Value = b.nroPrc;
                                cmdB.Parameters.Add("@soporte", SqlDbType.VarChar).Value = b.soporte;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.hojas123)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_Save_OtHoja123";
                                cmdB.Parameters.Add("@hoja123Id", SqlDbType.Int).Value = b.hoja123Id;
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@item", SqlDbType.Int).Value = b.item;

                                cmdB.Parameters.Add("@nroCelda", SqlDbType.VarChar).Value = b.nroCelda;
                                cmdB.Parameters.Add("@funcion", SqlDbType.VarChar).Value = b.funcion;
                                cmdB.Parameters.Add("@cliente", SqlDbType.VarChar).Value = b.cliente;
                                cmdB.Parameters.Add("@suminisrro", SqlDbType.VarChar).Value = b.suminisrro;
                                cmdB.Parameters.Add("@equipo", SqlDbType.VarChar).Value = b.equipo;
                                cmdB.Parameters.Add("@tipo_Fijo", SqlDbType.VarChar).Value = b.tipo_Fijo;
                                cmdB.Parameters.Add("@tipo_Extra", SqlDbType.VarChar).Value = b.tipo_Extraib;
                                cmdB.Parameters.Add("@subtipo", SqlDbType.VarChar).Value = b.subtipo;
                                cmdB.Parameters.Add("@kardex", SqlDbType.VarChar).Value = b.kardex;
                                cmdB.Parameters.Add("@nroFabrica", SqlDbType.VarChar).Value = b.nroFabrica;
                                cmdB.Parameters.Add("@marca", SqlDbType.VarChar).Value = b.marca;
                                cmdB.Parameters.Add("@modelo", SqlDbType.VarChar).Value = b.modelo;
                                cmdB.Parameters.Add("@inom", SqlDbType.VarChar).Value = b.inom;
                                cmdB.Parameters.Add("@mando", SqlDbType.VarChar).Value = b.mando;
                                cmdB.Parameters.Add("@rele", SqlDbType.VarChar).Value = b.rele;
                                cmdB.Parameters.Add("@irele", SqlDbType.VarChar).Value = b.irele;
                                cmdB.Parameters.Add("@tipo", SqlDbType.VarChar).Value = b.tipo;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.hojas4)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_Save_Hoja4";
                                cmdB.Parameters.Add("@hoja4Id", SqlDbType.Int).Value = b.hoja4Id;
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@item", SqlDbType.Int).Value = b.item;

                                cmdB.Parameters.Add("@nroTrafo", SqlDbType.VarChar).Value = b.nroTrafo;
                                cmdB.Parameters.Add("@ubicacion ", SqlDbType.VarChar).Value = b.ubicacion;
                                cmdB.Parameters.Add("@nroFabrica", SqlDbType.VarChar).Value = b.nroFabrica;
                                cmdB.Parameters.Add("@potInst", SqlDbType.VarChar).Value = b.potInst;
                                cmdB.Parameters.Add("@anio", SqlDbType.VarChar).Value = b.anio;
                                cmdB.Parameters.Add("@marca", SqlDbType.VarChar).Value = b.marca;
                                cmdB.Parameters.Add("@tipo", SqlDbType.VarChar).Value = b.tipo;
                                cmdB.Parameters.Add("@kardex", SqlDbType.VarChar).Value = b.kardex;
                                cmdB.Parameters.Add("@posicion", SqlDbType.VarChar).Value = b.posicion;
                                cmdB.Parameters.Add("@relTransf", SqlDbType.VarChar).Value = b.relTransf;
                                cmdB.Parameters.Add("@potenciaCC", SqlDbType.VarChar).Value = b.potenciaCC;
                                cmdB.Parameters.Add("@nroFases", SqlDbType.VarChar).Value = b.nroFases;
                                cmdB.Parameters.Add("@cableC1", SqlDbType.VarChar).Value = b.cableC1;
                                cmdB.Parameters.Add("@cableC2", SqlDbType.VarChar).Value = b.cableC2;
                                cmdB.Parameters.Add("@cableC3", SqlDbType.VarChar).Value = b.cableC3;
                                cmdB.Parameters.Add("@cableC4", SqlDbType.VarChar).Value = b.cableC4;
                                cmdB.Parameters.Add("@cableC5", SqlDbType.VarChar).Value = b.cableC5;
                                cmdB.Parameters.Add("@disMarca", SqlDbType.VarChar).Value = b.disMarca;
                                cmdB.Parameters.Add("@disKardex", SqlDbType.VarChar).Value = b.disKardex;
                                cmdB.Parameters.Add("@disSerie", SqlDbType.VarChar).Value = b.disSerie;
                                cmdB.Parameters.Add("@disIA", SqlDbType.VarChar).Value = b.disIA;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.hojas567)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_Sabe_Hoja567";
                                cmdB.Parameters.Add("@hoja56Id", SqlDbType.Int).Value = b.hoja56Id;
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@item", SqlDbType.Int).Value = b.item;
                                cmdB.Parameters.Add("@tipoTablero", SqlDbType.Int).Value = b.tipoTablero;
                                cmdB.Parameters.Add("@idtipo", SqlDbType.Int).Value = b.idtipo;
                                cmdB.Parameters.Add("@base", SqlDbType.VarChar).Value = b.baseMovil;
                                cmdB.Parameters.Add("@fusible", SqlDbType.VarChar).Value = b.fusible;
                                cmdB.Parameters.Add("@seccion", SqlDbType.VarChar).Value = b.seccion;
                                cmdB.Parameters.Add("@observacion", SqlDbType.VarChar).Value = b.observacion;
                                cmdB.Parameters.Add("@nroMedidor", SqlDbType.VarChar).Value = b.nroMedidor;
                                cmdB.Parameters.Add("@fotocelula", SqlDbType.VarChar).Value = b.fotocelula;
                                cmdB.Parameters.Add("@contactor", SqlDbType.VarChar).Value = b.contactor;
                                cmdB.Parameters.Add("@intHorario", SqlDbType.VarChar).Value = b.intHorario;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.protocolos)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_SaveProtocolo";
                                cmdB.Parameters.Add("@protocoloId", SqlDbType.Int).Value = b.protocoloId;
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@tipoProtocoloId", SqlDbType.Int).Value = b.tipoProtocoloId;

                                cmdB.Parameters.Add("@tipoTerreno", SqlDbType.VarChar).Value = b.tipoTerreno;
                                cmdB.Parameters.Add("@estadoTerreno", SqlDbType.VarChar).Value = b.estadoTerreno;
                                cmdB.Parameters.Add("@resistenciaSeca", SqlDbType.VarChar).Value = b.resistenciaSeca;
                                cmdB.Parameters.Add("@resistenciaHumeda", SqlDbType.VarChar).Value = b.resistenciaHumeda;
                                cmdB.Parameters.Add("@fechaSeca ", SqlDbType.VarChar).Value = b.fechaSeca;
                                cmdB.Parameters.Add("@aSeca1 ", SqlDbType.VarChar).Value = b.aSeca1;
                                cmdB.Parameters.Add("@aSeca2 ", SqlDbType.VarChar).Value = b.aSeca2;
                                cmdB.Parameters.Add("@aSeca3 ", SqlDbType.VarChar).Value = b.aSeca3;
                                cmdB.Parameters.Add("@fechaHumeda", SqlDbType.VarChar).Value = b.fechaHumeda;
                                cmdB.Parameters.Add("@aHumeda1 ", SqlDbType.VarChar).Value = b.aHumeda1;
                                cmdB.Parameters.Add("@aHumeda2 ", SqlDbType.VarChar).Value = b.aHumeda2;
                                cmdB.Parameters.Add("@aHumeda3 ", SqlDbType.VarChar).Value = b.aHumeda3;

                                cmdB.Parameters.Add("@rBt1", SqlDbType.VarChar).Value = b.rBt1;
                                cmdB.Parameters.Add("@pBt1", SqlDbType.VarChar).Value = b.pBt1;
                                cmdB.Parameters.Add("@rBt2", SqlDbType.VarChar).Value = b.rBt2;
                                cmdB.Parameters.Add("@pBt2", SqlDbType.VarChar).Value = b.pBt2;
                                cmdB.Parameters.Add("@rBt3", SqlDbType.VarChar).Value = b.rBt3;
                                cmdB.Parameters.Add("@pBt3", SqlDbType.VarChar).Value = b.pBt3;
                                cmdB.Parameters.Add("@rBt4", SqlDbType.VarChar).Value = b.rBt4;
                                cmdB.Parameters.Add("@pBt4", SqlDbType.VarChar).Value = b.pBt4;
                                cmdB.Parameters.Add("@rBtE", SqlDbType.VarChar).Value = b.rBtE;
                                cmdB.Parameters.Add("@pBtE", SqlDbType.VarChar).Value = b.pBtE;
                                cmdB.Parameters.Add("@rMt1", SqlDbType.VarChar).Value = b.rMt1;
                                cmdB.Parameters.Add("@pMt1", SqlDbType.VarChar).Value = b.pMt1;
                                cmdB.Parameters.Add("@rMt2", SqlDbType.VarChar).Value = b.rMt2;
                                cmdB.Parameters.Add("@pMt2", SqlDbType.VarChar).Value = b.pMt2;
                                cmdB.Parameters.Add("@rMt3", SqlDbType.VarChar).Value = b.rMt3;
                                cmdB.Parameters.Add("@pMt3", SqlDbType.VarChar).Value = b.pMt3;
                                cmdB.Parameters.Add("@rMt4", SqlDbType.VarChar).Value = b.rMt4;
                                cmdB.Parameters.Add("@pMt4", SqlDbType.VarChar).Value = b.pMt4;
                                cmdB.Parameters.Add("@rMtE", SqlDbType.VarChar).Value = b.rMtE;
                                cmdB.Parameters.Add("@pMtE", SqlDbType.VarChar).Value = b.pMtE;
                                cmdB.Parameters.Add("@observaciones", SqlDbType.VarChar).Value = b.observaciones;
                                cmdB.Parameters.Add("@proyecto", SqlDbType.VarChar).Value = b.proyecto;
                                cmdB.Parameters.Add("@tipoSistema", SqlDbType.VarChar).Value = b.tipoSistema;
                                cmdB.Parameters.Add("@grupoResis", SqlDbType.VarChar).Value = b.grupoResis;


                                cmdB.Parameters.Add("@nivelResis ", SqlDbType.VarChar).Value = b.nivelResis;
                                cmdB.Parameters.Add("@btNpozos ", SqlDbType.VarChar).Value = b.btNpozos;
                                cmdB.Parameters.Add("@btPozoTratado ", SqlDbType.VarChar).Value = b.btPozoTratado;
                                cmdB.Parameters.Add("@btNdosis ", SqlDbType.VarChar).Value = b.btNdosis;
                                cmdB.Parameters.Add("@btCompuesto ", SqlDbType.VarChar).Value = b.btCompuesto;
                                cmdB.Parameters.Add("@mtNpozos ", SqlDbType.VarChar).Value = b.mtNpozos;
                                cmdB.Parameters.Add("@mtPozoTratado ", SqlDbType.VarChar).Value = b.mtPozoTratado;
                                cmdB.Parameters.Add("@mtNdosis ", SqlDbType.VarChar).Value = b.mtNdosis;
                                cmdB.Parameters.Add("@mtCompuesto", SqlDbType.VarChar).Value = b.mtCompuesto;

                                cmdB.Parameters.Add("@fecha", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                                cmdB.ExecuteNonQuery();
                            }

                            foreach (var b in r.photos)
                            {
                                SqlCommand cmdB = con.CreateCommand();
                                cmdB.CommandTimeout = 0;
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.CommandText = "Movil_Save_Foto";
                                cmdB.Parameters.Add("@formatoId", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdB.Parameters.Add("@fotoUrl ", SqlDbType.VarChar).Value = b.fotoUrl;
                                cmdB.Parameters.Add("@observacion ", SqlDbType.VarChar).Value = b.observacion;
                                cmdB.Parameters.Add("@estado ", SqlDbType.Int).Value = b.estado;
                                cmdB.Parameters.Add("@usuarioId ", SqlDbType.Int).Value = b.usuarioId;
                                cmdB.Parameters.Add("@fecha ", SqlDbType.VarChar).Value = b.fecha;
                                cmdB.ExecuteNonQuery();
                            }
                        }
                    }

                    con.Close();
                }
                return m;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static Mensaje UpdateOt(Ot o)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Reasignacion_Ot";
                    cmd.Parameters.Add("@otId", SqlDbType.Int).Value = o.otId;
                    cmd.Parameters.Add("@motivoId", SqlDbType.Int).Value = o.motivoId;
                    cmd.Parameters.Add("@comentario", SqlDbType.VarChar).Value = o.comentario;
                    cmd.Parameters.Add("@estadoId", SqlDbType.Int).Value = o.estadoId;

                    int a = cmd.ExecuteNonQuery();

                    if (a != 0)
                    {
                        m = new Mensaje
                        {
                            codigoBase = o.otId,
                            mensaje = "Enviado"
                        };
                    }

                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveParteDiario(ParteDiario p)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Save_ParteDiario";
                    cmd.Parameters.Add("@scId", SqlDbType.Int).Value = p.identity;
                    cmd.Parameters.Add("@fecha_SalidaProgramada", SqlDbType.VarChar).Value = p.fechaSalida;
                    cmd.Parameters.Add("@horaInicio_SC", SqlDbType.VarChar).Value = p.horaInicio;
                    cmd.Parameters.Add("@horaFin_SC", SqlDbType.VarChar).Value = p.horaFin;
                    cmd.Parameters.Add("@sector_SC", SqlDbType.VarChar).Value = p.sector;
                    cmd.Parameters.Add("@id_Proyectista", SqlDbType.Int).Value = p.proyectistaId;
                    cmd.Parameters.Add("@id_TrabajoProgramado", SqlDbType.Int).Value = p.trabajoProgramadoId;
                    cmd.Parameters.Add("@id_SupervisorEnel", SqlDbType.Int).Value = p.supervisorId;
                    cmd.Parameters.Add("@id_Distrito", SqlDbType.Int).Value = p.distritoId;
                    cmd.Parameters.Add("@id_OT", SqlDbType.Int).Value = p.otId;
                    cmd.Parameters.Add("@nroSed_SC", SqlDbType.VarChar).Value = p.nroSed;
                    cmd.Parameters.Add("@fechaSalida_SC", SqlDbType.VarChar).Value = p.fechaSalida;
                    cmd.Parameters.Add("@id_Cuadrilla", SqlDbType.Int).Value = p.cuadrillaId;
                    cmd.Parameters.Add("@id_Placa", SqlDbType.Int).Value = p.placaId;
                    cmd.Parameters.Add("@observaciones_SC", SqlDbType.VarChar).Value = p.observaciones;
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = p.estado;
                    cmd.Parameters.Add("@usuarioId", SqlDbType.Int).Value = p.proyectistaId;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();

                        while (dr.Read())
                        {
                            m.mensaje = "Mensaje Enviado";
                            m.codigoBase = p.parteDiarioId;
                            m.codigoRetorno = dr.GetInt32(0);
                        }
                    }
                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveInspeccion(InspeccionPoste p)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DSIGE_Proy_M_Inspeccion_Proceso_1";
                    cmd.Parameters.Add("@id_Inspeccion_Campo", SqlDbType.Int).Value = p.inspeccionCampoId;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = p.usuarioId;
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = p.estadoId;
                    cmd.Parameters.Add("@utmX", SqlDbType.VarChar).Value = p.utmX;
                    cmd.Parameters.Add("@utmY", SqlDbType.VarChar).Value = p.utmY;
                    cmd.Parameters.Add("@id_MaterialSoporte", SqlDbType.Int).Value = p.id_MaterialSoporte;
                    cmd.Parameters.Add("@id_TipoPoste", SqlDbType.Int).Value = p.id_TipoPoste;
                    cmd.Parameters.Add("@id_CargaTrabajo", SqlDbType.Int).Value = p.id_CargaTrabajo;
                    cmd.Parameters.Add("@id_TipoArmado", SqlDbType.Int).Value = p.id_TipoArmado;
                    cmd.Parameters.Add("@alturaPoste", SqlDbType.Decimal).Value = p.alturaPoste;
                    cmd.Parameters.Add("@alturaLibrePoste", SqlDbType.Decimal).Value = p.alturaLibrePoste;
                    cmd.Parameters.Add("@id_estadoMensula", SqlDbType.Int).Value = p.id_estadoMensula;
                    cmd.Parameters.Add("@id_NivelCriticidad", SqlDbType.Int).Value = p.id_NivelCriticidad;
                    cmd.Parameters.Add("@comentariosGenerales", SqlDbType.VarChar).Value = p.comentariosGenerales;
                    //------------------------------------------
                    cmd.Parameters.Add("@vanoMT_AngIzq", SqlDbType.Decimal).Value = p.vanoMT_AngIzq;
                    cmd.Parameters.Add("@vanoMT_AngDer", SqlDbType.Decimal).Value = p.vanoMT_AngDer;
                    cmd.Parameters.Add("@vanoMT_AngAde", SqlDbType.Decimal).Value = p.vanoMT_AngAde;
                    cmd.Parameters.Add("@vanoMT_AngAtra", SqlDbType.Decimal).Value = p.vanoMT_AngAtra;
                    cmd.Parameters.Add("@vanoMT_VanIzq", SqlDbType.Decimal).Value = p.vanoMT_VanIzq;
                    cmd.Parameters.Add("@vanoMT_VanDer", SqlDbType.Decimal).Value = p.vanoMT_VanDer;
                    cmd.Parameters.Add("@vanoMT_VanAde", SqlDbType.Decimal).Value = p.vanoMT_VanAde;
                    cmd.Parameters.Add("@vanoMT_VanAtra", SqlDbType.Decimal).Value = p.vanoMT_VanAtra;
                    cmd.Parameters.Add("@comentariosVanoMt", SqlDbType.VarChar).Value = p.comentariosVanoMt;
                    //------------------------------------------
                    cmd.Parameters.Add("@vanoBT_AngIzq", SqlDbType.Decimal).Value = p.vanoBT_AngIzq;
                    cmd.Parameters.Add("@vanoBT_AngDer", SqlDbType.Decimal).Value = p.vanoBT_AngDer;
                    cmd.Parameters.Add("@vanoBT_AngAde", SqlDbType.Decimal).Value = p.vanoBT_AngAde;
                    cmd.Parameters.Add("@vanoBT_AngAtra", SqlDbType.Decimal).Value = p.vanoBT_AngAtra;
                    cmd.Parameters.Add("@vanoBT_VanIzq", SqlDbType.Decimal).Value = p.vanoBT_VanIzq;
                    cmd.Parameters.Add("@vanoBT_VanDer", SqlDbType.Decimal).Value = p.vanoBT_VanDer;
                    cmd.Parameters.Add("@vanoBT_VanAde", SqlDbType.Decimal).Value = p.vanoBT_VanAde;
                    cmd.Parameters.Add("@vanoBT_VanAtra", SqlDbType.Decimal).Value = p.vanoBT_VanAtra;
                    cmd.Parameters.Add("@AngRetenidaBt", SqlDbType.Decimal).Value = p.AngRetenidaBt;
                    cmd.Parameters.Add("@comentariosVanoBt", SqlDbType.VarChar).Value = p.comentariosVanoBt;
                    //--------------------------------------------
                    cmd.Parameters.Add("@id_reteBT_tipoR1", SqlDbType.Int).Value = p.id_reteBT_tipoR1;
                    cmd.Parameters.Add("@reteBT_AlturaR1", SqlDbType.Decimal).Value = p.reteBT_AlturaR1;
                    cmd.Parameters.Add("@reteBT_DirR1", SqlDbType.VarChar).Value = p.reteBT_DirR1;
                    cmd.Parameters.Add("@id_reteBT_Estado1", SqlDbType.Int).Value = p.id_reteBT_Estado1;
                    cmd.Parameters.Add("@id_reteBT_tipoR2", SqlDbType.Int).Value = p.id_reteBT_tipoR2;
                    cmd.Parameters.Add("@reteBT_AlturaR2", SqlDbType.Decimal).Value = p.reteBT_AlturaR2;
                    cmd.Parameters.Add("@reteBT_DirR2", SqlDbType.VarChar).Value = p.reteBT_DirR2;
                    cmd.Parameters.Add("@id_reteBT_Estado2", SqlDbType.Int).Value = p.id_reteBT_Estado2;
                    cmd.Parameters.Add("@comentarios_ReteBT", SqlDbType.VarChar).Value = p.comentarios_ReteBT;
                    //----------------------------------------------
                    cmd.Parameters.Add("@infoAdd_RolloReserva", SqlDbType.VarChar).Value = p.infoAdd_RolloReserva;
                    cmd.Parameters.Add("@infoAdd_Fotos", SqlDbType.VarChar).Value = p.infoAdd_Fotos;
                    cmd.Parameters.Add("@id_infoAdd_Consecion", SqlDbType.Int).Value = p.id_infoAdd_Consecion;
                    cmd.Parameters.Add("@infoAdd_Niveltension", SqlDbType.VarChar).Value = p.infoAdd_Niveltension;
                    cmd.Parameters.Add("@infoCnew_CableNuevo", SqlDbType.VarChar).Value = p.infoCnew_CableNuevo;
                    cmd.Parameters.Add("@infoCnew_vanoIzq", SqlDbType.Decimal).Value = p.infoCnew_vanoIzq;
                    cmd.Parameters.Add("@infoCnew_vanoDer", SqlDbType.Decimal).Value = p.infoCnew_vanoDer;
                    cmd.Parameters.Add("@infoCnew_vanoAde", SqlDbType.Decimal).Value = p.infoCnew_vanoAde;
                    cmd.Parameters.Add("@infoCnew_vanoAtra", SqlDbType.Decimal).Value = p.infoCnew_vanoAtra;
                    cmd.Parameters.Add("@infoCnew_alturaInstala", SqlDbType.Decimal).Value = p.infoCnew_alturaInstala;
                    //----------------------------------------------
                    cmd.Parameters.Add("@obsC_Cumplimiento", SqlDbType.Int).Value = p.obsC_Cumplimiento;
                    cmd.Parameters.Add("@obsC_PosteInclinado", SqlDbType.Int).Value = p.obsC_PosteInclinado;
                    cmd.Parameters.Add("@obsC_PosteSubida", SqlDbType.Int).Value = p.obsC_PosteSubida;
                    cmd.Parameters.Add("@obsC_PosteSaturado", SqlDbType.Int).Value = p.obsC_PosteSaturado;
                    cmd.Parameters.Add("@comentarios_obsC", SqlDbType.VarChar).Value = p.comentarios_obsC;
                    cmd.Parameters.Add("@resFac_Factible", SqlDbType.Int).Value = p.resFac_Factible;
                    cmd.Parameters.Add("@id_resFac_ObsPrincipal", SqlDbType.Int).Value = p.id_resFac_ObsPrincipal;
                    cmd.Parameters.Add("@Obs_generales", SqlDbType.VarChar).Value = p.Obs_generales;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();

                        while (dr.Read())
                        {
                            m.mensaje = "Mensaje Enviado";
                            m.codigoBase = p.inspeccionCampoId;
                            m.codigoRetorno = dr.GetInt32(0);

                            var c = p.cable;
                            if (c != null)
                            {
                                SqlCommand cmd1 = cn.CreateCommand();
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.CommandText = "DSIGE_Proy_M_Inspeccion_Cable";
                                cmd1.Parameters.Add("@id_Inspeccion_Cable", SqlDbType.Int).Value = c.identity;
                                cmd1.Parameters.Add("@id_Inspeccion_Campo", SqlDbType.Int).Value = p.inspeccionCampoId;
                                cmd1.Parameters.Add("@usuario", SqlDbType.Int).Value = p.usuarioId;
                                cmd1.Parameters.Add("@estado", SqlDbType.Int).Value = p.estadoId;
                                cmd1.Parameters.Add("@circuitoBT", SqlDbType.Decimal).Value = c.circuitoBT;
                                cmd1.Parameters.Add("@condIzqTipo1", SqlDbType.Int).Value = c.condIzqTipo1;
                                cmd1.Parameters.Add("@condIzqAltura1_BT", SqlDbType.Decimal).Value = c.condIzqAltura1_BT;
                                cmd1.Parameters.Add("@condIzqTipo2", SqlDbType.Int).Value = c.condIzqTipo2;
                                cmd1.Parameters.Add("@condIzqAltura2_BT", SqlDbType.Decimal).Value = c.condIzqAltura2_BT;
                                cmd1.Parameters.Add("@cableBT", SqlDbType.Decimal).Value = c.cableBT;
                                cmd1.Parameters.Add("@condDerTipo1", SqlDbType.Int).Value = c.condDerTipo1;
                                cmd1.Parameters.Add("@condDerAltura1_BT", SqlDbType.Decimal).Value = c.condDerAltura1_BT;
                                cmd1.Parameters.Add("@condDerTipo2", SqlDbType.Int).Value = c.condDerTipo2;
                                cmd1.Parameters.Add("@condDerAltura2_BT", SqlDbType.Decimal).Value = c.condDerAltura2_BT;
                                cmd1.Parameters.Add("@longbrazo", SqlDbType.Decimal).Value = c.longbrazo;
                                cmd1.Parameters.Add("@condAdeTipo1", SqlDbType.Int).Value = c.condAdeTipo1;
                                cmd1.Parameters.Add("@condAdeAltura1_BT", SqlDbType.Decimal).Value = c.condAdeAltura1_BT;
                                cmd1.Parameters.Add("@condAdeTipo2", SqlDbType.Int).Value = c.condAdeTipo2;
                                cmd1.Parameters.Add("@condAdeAltura2_BT", SqlDbType.Decimal).Value = c.condAdeAltura2_BT;
                                cmd1.Parameters.Add("@condAtrasTipo1", SqlDbType.Int).Value = c.condAtrasTipo1;
                                cmd1.Parameters.Add("@condAtrasAltura1_BT", SqlDbType.Decimal).Value = c.condAtrasAltura1_BT;
                                cmd1.Parameters.Add("@condAtrasTipo2", SqlDbType.Int).Value = c.condAtrasTipo2;
                                cmd1.Parameters.Add("@condAtrasAltura2_BT", SqlDbType.Decimal).Value = c.condAtrasAltura2_BT;
                                cmd1.Parameters.Add("@comentarioCableBT", SqlDbType.VarChar).Value = c.comentarioCableBT;
								//------------------------------------------------------------
								cmd1.Parameters.Add("@tipoCable1", SqlDbType.VarChar).Value = c.tipoCable1;
								cmd1.Parameters.Add("@condIzqCant1", SqlDbType.Decimal).Value = c.condIzqCant1;
								cmd1.Parameters.Add("@condIzqAltura1_Te", SqlDbType.Decimal).Value = c.condIzqAltura1_Te;
								cmd1.Parameters.Add("@condIzqCant2", SqlDbType.Decimal).Value = c.condIzqCant2;
								cmd1.Parameters.Add("@condIzqAltura2_Te", SqlDbType.Decimal).Value = c.condIzqAltura2_Te;
								cmd1.Parameters.Add("@tipoCable2", SqlDbType.VarChar).Value = c.tipoCable2;
								cmd1.Parameters.Add("@condDerCant1", SqlDbType.Decimal).Value = c.condDerCant1;
								cmd1.Parameters.Add("@condDerAltura1_Te", SqlDbType.Decimal).Value = c.condDerAltura1_Te;
								cmd1.Parameters.Add("@condDerCant2", SqlDbType.Decimal).Value = c.condDerCant2;
								cmd1.Parameters.Add("@condDerAltura2_Te", SqlDbType.Decimal).Value = c.condDerAltura2_Te;
								cmd1.Parameters.Add("@cableAdss", SqlDbType.Decimal).Value = c.cableAdss;
								cmd1.Parameters.Add("@condAdeCant1", SqlDbType.Decimal).Value = c.condAdeCant1;
								cmd1.Parameters.Add("@condAdeAltura1_Te", SqlDbType.Decimal).Value = c.condAdeAltura1_Te;
								cmd1.Parameters.Add("@condAdeCant2", SqlDbType.Decimal).Value = c.condAdeCant2;
								cmd1.Parameters.Add("@condAdeAltura2_Te", SqlDbType.Decimal).Value = c.condAdeAltura2_Te;
								cmd1.Parameters.Add("@cableCoaxial", SqlDbType.VarChar).Value = c.cableCoaxial;
								cmd1.Parameters.Add("@condAtrasCant1", SqlDbType.Decimal).Value = c.condAtrasCant1;
								cmd1.Parameters.Add("@condAtrasAltura1_Te", SqlDbType.Decimal).Value = c.condAtrasAltura1_Te;
								cmd1.Parameters.Add("@condAtrasCant2", SqlDbType.Decimal).Value = c.condAtrasCant2;
								cmd1.Parameters.Add("@condAtrasAltura2_Te", SqlDbType.Decimal).Value = c.condAtrasAltura2_Te;
								cmd1.Parameters.Add("@otrosCables", SqlDbType.VarChar).Value = c.otrosCables;
								cmd1.Parameters.Add("@longCant1", SqlDbType.Decimal).Value = c.longCant1;
								cmd1.Parameters.Add("@longAltura1_Te", SqlDbType.Decimal).Value = c.longAltura1_Te;
								cmd1.Parameters.Add("@longCant2", SqlDbType.Decimal).Value = c.longCant2;
								cmd1.Parameters.Add("@longAltura2_Te", SqlDbType.Decimal).Value = c.longAltura2_Te;
								cmd1.Parameters.Add("@comentarioTele", SqlDbType.VarChar).Value = c.comentarioTele;									
                                SqlDataReader dr1 = cmd1.ExecuteReader();
                                if (dr1.HasRows)
                                {
                                    while (dr1.Read())
                                    {
                                        m.codigoRetornoCable = dr1.GetInt32(0);
                                    }
                                }
                            }

                            var co = p.conductor;
                            if (co != null)
                            {
                                SqlCommand cmd2 = cn.CreateCommand();
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.CommandText = "DSIGE_Proy_M_Inspeccion_Conductor";
                                cmd2.Parameters.Add("@id_Inspeccion_ConductorMt", SqlDbType.Int).Value = co.identity;
                                cmd2.Parameters.Add("@id_Inspeccion_Campo", SqlDbType.Int).Value = co.inspeccionCampoId;
                                cmd2.Parameters.Add("@usuario", SqlDbType.Int).Value = p.usuarioId;
                                cmd2.Parameters.Add("@estado", SqlDbType.Int).Value = p.estadoId;
                                cmd2.Parameters.Add("@id_ternaMT", SqlDbType.Int).Value = co.id_ternaMT;
                                cmd2.Parameters.Add("@id_nCond", SqlDbType.Int).Value = co.id_nCond;
                                cmd2.Parameters.Add("@id_TipoArmado", SqlDbType.Int).Value = co.id_TipoArmado;
                                cmd2.Parameters.Add("@seccionIzq", SqlDbType.Decimal).Value = co.seccionIzq;
                                cmd2.Parameters.Add("@seccionDer", SqlDbType.Decimal).Value = co.seccionDer;
                                //--------------------------------------
                                cmd2.Parameters.Add("@seccionAdel", SqlDbType.Decimal).Value = co.seccionAdel;
                                cmd2.Parameters.Add("@seccionAtras", SqlDbType.Decimal).Value = co.seccionAtras;
                                cmd2.Parameters.Add("@vanoIzq", SqlDbType.Decimal).Value = co.vanoIzq;
                                cmd2.Parameters.Add("@vanoDer", SqlDbType.Decimal).Value = co.vanoDer;
                                cmd2.Parameters.Add("@vanoAdel", SqlDbType.Decimal).Value = co.vanoAdel;
                                cmd2.Parameters.Add("@vanoAtras", SqlDbType.Decimal).Value = co.vanoAtras;
                                cmd2.Parameters.Add("@alturaIzq", SqlDbType.Decimal).Value = co.alturaIzq;
                                cmd2.Parameters.Add("@alturaDer", SqlDbType.Decimal).Value = co.alturaDer;
                                cmd2.Parameters.Add("@alturaAdel", SqlDbType.Decimal).Value = co.alturaAdel;
                                cmd2.Parameters.Add("@alturaAtras", SqlDbType.Decimal).Value = co.alturaAtras;
                                cmd2.Parameters.Add("@distanciaIzq", SqlDbType.Decimal).Value = co.distanciaIzq;
                                cmd2.Parameters.Add("@distanciaDer", SqlDbType.Decimal).Value = co.distanciaDer;
                                cmd2.Parameters.Add("@distanciaAdel", SqlDbType.Decimal).Value = co.distanciaAdel;
                                cmd2.Parameters.Add("@distanciaAtras", SqlDbType.Decimal).Value = co.distanciaAtras;
                                //----------------------------------
                                cmd2.Parameters.Add("@retIzq_1", SqlDbType.Decimal).Value = co.retIzq_1;
                                cmd2.Parameters.Add("@retIzq_2", SqlDbType.Decimal).Value = co.retIzq_2;
                                cmd2.Parameters.Add("@retIzq_3", SqlDbType.Decimal).Value = co.retIzq_3;
                                cmd2.Parameters.Add("@retIzq_Estado", SqlDbType.Int).Value = co.retIzq_Estado;
                                cmd2.Parameters.Add("@retDer_1", SqlDbType.Decimal).Value = co.retDer_1;
                                cmd2.Parameters.Add("@retDer_2", SqlDbType.Decimal).Value = co.retDer_2;
                                cmd2.Parameters.Add("@retDer_3", SqlDbType.Decimal).Value = co.retDer_3;
                                cmd2.Parameters.Add("@retDer_Estado", SqlDbType.Int).Value = co.retDer_Estado;
                                cmd2.Parameters.Add("@retAtras_1", SqlDbType.Decimal).Value = co.retAtras_1;
                                cmd2.Parameters.Add("@retAtras_2", SqlDbType.Decimal).Value = co.retAtras_2;
                                cmd2.Parameters.Add("@retAtras_3", SqlDbType.Decimal).Value = co.retAtras_3;
                                cmd2.Parameters.Add("@retAtras_Estado", SqlDbType.Int).Value = co.retAtras_Estado;
                                cmd2.Parameters.Add("@retAde_1", SqlDbType.Decimal).Value = co.retAde_1;
                                cmd2.Parameters.Add("@retAde_2", SqlDbType.Decimal).Value = co.retAde_2;
                                cmd2.Parameters.Add("@retAde_3", SqlDbType.Decimal).Value = co.retAde_3;
                                cmd2.Parameters.Add("@retAde_Estado", SqlDbType.Int).Value = co.retAde_Estado;
                                cmd2.Parameters.Add("@comentario", SqlDbType.VarChar).Value = co.comentario;
                                SqlDataReader dr2 = cmd2.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        m.codigoRetornoConductor = dr2.GetInt32(0);
                                    }
                                }
                            }
                            var e = p.equipo;
                            if (e != null)
                            {
                                SqlCommand cmd3 = cn.CreateCommand();
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.CommandText = "DSIGE_Proy_M_Inspeccion_Equipo";
                                cmd3.Parameters.Add("@id_Inspeccion_Equipo", SqlDbType.Int).Value = e.identity;
                                cmd3.Parameters.Add("@id_Inspeccion_Campo", SqlDbType.Int).Value = e.inspeccionCampoId;
                                cmd3.Parameters.Add("@usuario", SqlDbType.Int).Value = p.usuarioId;
                                cmd3.Parameters.Add("@estado", SqlDbType.Int).Value = p.estadoId;
                                cmd3.Parameters.Add("@electrico1", SqlDbType.VarChar).Value = e.electrico1;
                                cmd3.Parameters.Add("@cantidad1", SqlDbType.Decimal).Value = e.cantidad1;
                                cmd3.Parameters.Add("@telecomunica1", SqlDbType.VarChar).Value = e.telecomunica1;
                                cmd3.Parameters.Add("@telecantidad1", SqlDbType.Decimal).Value = e.telecantidad1;
                                cmd3.Parameters.Add("@electrico2", SqlDbType.VarChar).Value = e.electrico2;
                                cmd3.Parameters.Add("@cantidad2", SqlDbType.Decimal).Value = e.cantidad2;
                                cmd3.Parameters.Add("@telecomunica2", SqlDbType.VarChar).Value = e.telecomunica2;
                                cmd3.Parameters.Add("@telecantidad2", SqlDbType.Decimal).Value = e.telecantidad2;
                                cmd3.Parameters.Add("@electrico3", SqlDbType.VarChar).Value = e.electrico3;
                                cmd3.Parameters.Add("@cantidad3", SqlDbType.Decimal).Value = e.cantidad3;
                                cmd3.Parameters.Add("@telecomunica3", SqlDbType.VarChar).Value = e.telecomunica3;
                                cmd3.Parameters.Add("@telecantidad3", SqlDbType.Decimal).Value = e.telecantidad3;
                                cmd3.Parameters.Add("@electrico4", SqlDbType.VarChar).Value = e.electrico4;
                                cmd3.Parameters.Add("@cantidad4", SqlDbType.Decimal).Value = e.cantidad4;
                                cmd3.Parameters.Add("@telecomunica4", SqlDbType.VarChar).Value = e.telecomunica4;
                                cmd3.Parameters.Add("@telecantidad4", SqlDbType.Decimal).Value = e.telecantidad4;
                                SqlDataReader dr3 = cmd3.ExecuteReader();
                                if (dr3.HasRows)
                                {
                                    while (dr3.Read())
                                    {
                                        m.codigoRetornoEquipo = dr3.GetInt32(0);
                                    }
                                }
                            }

                            foreach (var f in p.photos)
                            {
                                SqlCommand cmdF = cn.CreateCommand();
                                cmdF.CommandTimeout = 0;
                                cmdF.CommandType = CommandType.StoredProcedure;
                                cmdF.CommandText = "Movil_SaveInspeccionesFoto";
                                cmdF.Parameters.Add("@inspeccionCampoId", SqlDbType.Int).Value = p.inspeccionCampoId;
                                cmdF.Parameters.Add("@fotoUrl ", SqlDbType.VarChar).Value = f.fotoUrl;
                                cmdF.Parameters.Add("@estado ", SqlDbType.Int).Value = f.estado;
                                cmdF.Parameters.Add("@usuarioId ", SqlDbType.Int).Value = f.usuarioId;
                                cmdF.Parameters.Add("@fecha ", SqlDbType.VarChar).Value = f.fecha;
                                cmdF.ExecuteNonQuery();
                            }
                        }
                    }
                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveEstadoMovil(EstadoMovil e)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "Movil_SaveEstadoCelular";
                    cmd.Parameters.Add("@operarioId", SqlDbType.VarChar).Value = e.operarioId;
                    cmd.Parameters.Add("@gpsActivo", SqlDbType.Bit).Value = e.gpsActivo;
                    cmd.Parameters.Add("@estadoBateria", SqlDbType.Int).Value = e.estadoBateria;
                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = e.fecha;
                    cmd.Parameters.Add("@modoAvion", SqlDbType.Int).Value = e.modoAvion;
                    cmd.Parameters.Add("@planDatos", SqlDbType.Bit).Value = e.planDatos;
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        m = new Mensaje
                        {
                            codigoBase = 1,
                            mensaje = "Enviado"
                        };
                    }

                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveOperarioGps(EstadoOperario e)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_SaveGps";
                    cmd.Parameters.Add("@operarioId", SqlDbType.VarChar).Value = e.operarioId;
                    cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = e.latitud;
                    cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = e.longitud;
                    cmd.Parameters.Add("@fechaGPD", SqlDbType.VarChar).Value = e.fechaGPD;
                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = e.fecha;
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        m = new Mensaje
                        {
                            codigoBase = 1,
                            mensaje = "Enviado"
                        };
                    }

                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje UpdateEstadoOt(int id, int estado)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DSIGE_Proy_M_Update_Estado";
                    cmd.Parameters.Add("@otId", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@estadoId", SqlDbType.VarChar).Value = estado;
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        m = new Mensaje
                        {
                            codigoBase = id,
                            mensaje = "Actualizado"
                        };
                    }

                    cn.Close();
                }

                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}

