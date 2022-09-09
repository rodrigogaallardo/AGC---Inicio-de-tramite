
CREATE NONCLUSTERED INDEX [IX_Encomienda_id_solicitud]
ON [dbo].[Encomienda] ([id_solicitud])

CREATE NONCLUSTERED INDEX [IX_SSIT_Solicitudes_CreateUser]
ON [dbo].[SSIT_Solicitudes] ([CreateUser])
INCLUDE ([id_solicitud])
