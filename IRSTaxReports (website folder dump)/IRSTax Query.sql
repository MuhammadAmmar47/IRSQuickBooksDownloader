SELECT fldTypeOfForm,fldtaxyear2008,fldtaxyear2007,fldtaxyear2006,fldtaxyear2005,fldtaxyear2004,fldStatus,Customer.UserId,fldRequestName,fldssnno,fldDeliveryDate,'cuName'= Customer.Name,'cuID'=Customer.userId,fldLoannumber,Customer.irs_fee,Customer.ssn_fee,rushRate
from tblOrder inner join customer on tblOrder.fldCustomerID = customer.CustomerID 
	where (fldtypeofform < "&size&" or fldtypeofform = 6) and Customer.userId IN ("& AllUserId &") and fldDeliveryDate BETWEEN '" & dtFrom & "' AND '" & dtTo & "' 
	order by cuID