----- Melia Script ----------------------------------------------------------
-- Dialog
----- Description -----------------------------------------------------------
-- Collection of commonly used dialog functions.
-----------------------------------------------------------------------------

--- Extension of select that supports named indices.
-- The options can be prefixed with a string, separated with a colon,
-- which become the first return value, the second being the actual index
-- selected. If an option doesn't have a name, it defaults to __unnamedX.
-- @tparam  string  msg  The message to display.
-- @tparam  string  ...  The selectable options.
-- @treturn string  The name of the selected index.
-- @treturn int     The selected index.
-- @usage local n,i = nselect("How are you?", "g:Good", "foo:Bad", "Cancel")
function nselect(msg, ...)
	local args = type(...) == "table" and ... or {...}
	local keys = {}
	local vals = {}
	local n = 1

	-- Get keys and values
	for i=1,#args do
		local arg = args[i]
		local index = string.find(arg, ':')
		local key, val

		if index == nil then
			-- Fallback to "__unnamedX" if no colon was found
			key = "__unnamed" .. n
			val = arg
			n = n + 1
		else
			-- Get sub strings if there was a colon
			key = string.sub(arg, 1, index - 1)
			val = string.sub(arg, index + 1)
		end

		-- Save key and val for later
		table.insert(keys, key)
		table.insert(vals, val)
	end

	-- Get result from select
	local selectedindex = select(msg, unpack(vals))
	local optionname = keys[selectedindex]
	
	return optionname, selectedindex
end

--- Returns the amount of silver the character posseses.
-- Shortcut for countitem(SILVER).
-- @treturn int Amount of silver.
-- @usage local silver = countsilver()
function countsilver()
	return countitem(SILVER)
end

--- Adds the given amount of silver to the character's inventory.
-- Shortcut for additem(SILVER, amount).
-- @usage addsilver(1000)
function addsilver(amount)
	additem(SILVER, amount)
end

--- Removes the given amount of silver from the character's inventory.
-- Shortcut for removeitem(SILVER, amount).
-- @treturn int Amount removed.
-- @usage local removed = removesilver(1000)
function removesilver(amount)
	return removeitem(SILVER, amount)
end

--- Wraps string with the dicID code.
-- @tparam  string String to wrap.
-- @treturn string Wrapped string.
-- @usage dict("ETC_20150317_004443")
function dict(str)
	return "@dicID_^*$" .. str .. "$*^"
end

--- Wraps string with the auto code.
-- @tparam  string String to wrap.
-- @treturn string Wrapped string.
-- @usage auto("JongLyo")
function auto(str)
	return "!@#$Auto_" .. str .. "#@!" 
end

--- Wraps string with the dicID code and prepends the nselect key.
-- @tparam  string Key for nselect.
-- @tparam  string String to wrap.
-- @treturn string Wrapped string.
-- @usage ndict("test", "ETC_20150317_004443")
function ndict(key, str)
	return key .. ":" .. dict(str)
end

--- Wraps string with the auto code and prepends the nselect key.
-- @tparam  string Key for nselect.
-- @tparam  string String to wrap.
-- @treturn string Wrapped string.
-- @usage nauto("test", "JongLyo")
function nauto(key, str)
	return key .. ":" .. auto(str)
end
