select * from public.body_types;
select * from public.sizes;
select * from public.makes;

select * from public.models;
select * from public.model_styles;
select * from public.model_style_years;

select m.id as model_id, mk.id as make_id, msy.year, mk.name as make, m.name as model, bt.name as body_type, s.name as size
from public.models as m
inner join public.model_styles as ms on ms.model_id = m.id
inner join public.model_style_years as msy on msy.model_style_id = ms.id
inner join public.makes as mk on mk.id = m.make_id
inner join public.body_types as bt on bt.id = ms.body_type_id
inner join public.sizes as s on s.id = ms.size_id
order by mk.name, m.name, bt.name, s.name, msy.year


select * from public.quote_overides;
select * from public.quote_rules;

select * from public.quotes;

TRUNCATE public.body_types, public.sizes, public.quotes, public.quote_overides, public.model_style_years, public.model_styles RESTART IDENTITY;

